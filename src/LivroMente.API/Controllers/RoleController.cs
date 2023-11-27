using LivroMente.Domain.Models.Dto;
using LivroMente.Domain.Models.IdentityEntities;
using LivroMente.Domain.Models.RoleModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LivroMente.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
         private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IRoleRepository<Role> _roleRepository;

        public RoleController(RoleManager<Role> roleManager, UserManager<User> userManager,IRoleRepository<Role> roleRepository)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _roleRepository = roleRepository;
        }

        // GET: api/Role
        [HttpGet] 
        [Authorize(Roles = "Admin")]
        //[AllowAnonymous] 
        public async Task<IActionResult> Get()
        {
            var entity = await _roleRepository.GetAll();
            return Ok(entity);
        }


        // POST: api/Role/CreateRole
        [HttpPost("CreateRole")]
        [AllowAnonymous]        
        public async Task<IActionResult> CreateRole(RoleDto roleDto)
        {
            try
            {
                var retorno = await _roleManager.CreateAsync(new Role { Name = roleDto.Name });

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"ERROR {ex.Message}");
            }
        }

        [HttpPut("UpdateUserRole")]
        [AllowAnonymous] 
        public async Task<IActionResult> UpdateUserRoles(UpdateUserRoleDto model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    if (model.Delete)
                        await _userManager.RemoveFromRoleAsync(user, model.Role);
                    else
                        await _userManager.AddToRoleAsync(user, model.Role);
                }
                else
                {
                    return Ok("Usuário não encontrado");
                }

                return Ok("Sucesso");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"ERROR {ex.Message}");
            }
        }

        //t
    }
    
}
