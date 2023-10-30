using System.Globalization;
using AutoMapper;
using LivroMente.Domain.Models.AdressModel;
using LivroMente.Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LivroMente.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdressController : ControllerBase
    {
        private readonly IAdressRepository _AdressRepository;
        private readonly IMapper _mapper;

        public AdressController(IAdressRepository AdressRepository,IMapper mapper)
        {
            _AdressRepository = AdressRepository;
            _mapper = mapper;   
        }

        [HttpPost]

        public async Task<IActionResult> Post(AdressViewModel adress)
        {
            var adress1 = _mapper.Map<Adress>(adress);

            var lista =  await _AdressRepository.GetAll();

            lista.ToList();

              foreach (var item in lista)
              {
               bool verificaState = adress1.State.Contains(item.State,StringComparison.OrdinalIgnoreCase);
               bool verificaNumber = adress1.Number.Contains(item.Number,StringComparison.OrdinalIgnoreCase);
               if(verificaState && verificaNumber){
                 return BadRequest("Já existe no banco de dados");
                }
              }

                //Deixa Primeira letra Maiúscula 
              // adress1.Descripti = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(cat1.Description);

             _AdressRepository.Add(adress1);

             if (await _AdressRepository.UnitOfWork.SaveChangesAsync() > 0)
                return Created($"api/Adress/{adress.Id}", adress);
            return BadRequest();
        }

        [HttpGet]
        public  async Task<IActionResult> GetAllAdress()
        {
            var adress = await  _AdressRepository.GetAll();
            return Ok(adress);
        }

        [HttpGet ("{AdressId}")]
        public  IActionResult GetById(Guid AdressId)
        {
            var entity =   _AdressRepository.GetbyId(AdressId);
            return Ok(entity);
            
        }

        [HttpPut("{AdressId}")]
        public async Task<IActionResult> Put(Guid AdressId,AdressViewModel adress)
        {
             var entity =  _AdressRepository.GetbyId(AdressId);

            if (entity == null) return NotFound();
            _mapper.Map(adress, entity);
            
             //Deixa Primeira letra Maiúscula
           // entity.Description = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(entity.Description);

            _AdressRepository.Update(entity);

            if(await _AdressRepository.UnitOfWork.SaveChangesAsync() > 0)
          
            return Created($"api/Adress/{adress.Id}", _mapper.Map<AdressViewModel>(entity));
            return BadRequest();
        }

        [HttpDelete ("{AdressId}")]
        public async Task<IActionResult> Delete(Guid AdressId)
        {
              var entity =  _AdressRepository.GetbyId(AdressId);

            if (entity == null) return NotFound();
            _AdressRepository.Delete(entity);

            if (await _AdressRepository.UnitOfWork.SaveChangesAsync() > 0)
            return Ok();
            return BadRequest(); 

        }
    }
}