using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using LivroMente.Domain.Models.Dto;
using LivroMente.Domain.Models.IdentityEntities;
using LivroMente.Domain.Models.UserModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Calhas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository<User> _userRepository;
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public UserController(IUserRepository<User> userRepository,IConfiguration config,UserManager<User> userManager,
                              SignInManager<User> signInManager,IMapper mapper)
        {
            _userRepository = userRepository;
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var entity = _userRepository.GetUserRolesInclude();

                return Ok(entity);
                //return Ok(await _service.GetAll());
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                 $"Banco dados Falhou: {ex.Message} ");
            }
        }

       
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserDto userDto)
        {
           try
           {
            var user = await _userManager.FindByNameAsync(userDto.UserName);

                if (user == null)
                {
                    user = new User
                    {
                        UserName = userDto.UserName,
                        Email = userDto.Email,
                    };

                    
                    var result = await _userManager.CreateAsync(
                        user, userDto.Password
                    );

                    if(result.Succeeded){
                        var appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == user.UserName.ToUpper());
                        var token = GenerateJWToken(appUser).Result;
                        return Ok(token);
                    }
               
                }

                 return Unauthorized();
           }
           catch (System.Exception ex)
           {
             Console.WriteLine("Erro ao salvar as alterações: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Exceção interna: " + ex.InnerException.Message);
                }
            return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error {ex.Message}");
           }
                
           
        }

         private async Task<string> GenerateJWToken(User user)
        {
           var claims = new List<Claim> 
           {
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim(ClaimTypes.Name,user.UserName)
           };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role,role));
            }

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescription);

            return tokenHandler.WriteToken(token);
        }

        [HttpPost("Login")]
        //[AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto userLogin)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userLogin.UserName);

                var result = await _signInManager.CheckPasswordSignInAsync(user, userLogin.Password, false);

                if(result.Succeeded)
                {
                    var appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == user.UserName.ToUpper());

                    var userToReturn = _mapper.Map<UserLoginDto>(appUser);

                    return Ok(new {
                        token = GenerateJWToken(appUser).Result,
                        user = appUser
                    }); 
                }

                return Unauthorized();
            }
            catch (System.Exception ex)
           {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Error {ex.Message}");
           }
        }


    }
}