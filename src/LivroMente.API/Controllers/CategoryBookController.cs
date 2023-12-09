using System.Globalization;
using AutoMapper;
using LivroMente.Domain.Models.CategoryBookModel;
using LivroMente.Domain.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LivroMente.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryBookController : ControllerBase
    {
          private readonly ICategoryBookRepository _categoryBookRepository;
          private readonly IMapper _mapper;

        public CategoryBookController(ICategoryBookRepository categoryBookRepository,IMapper mapper)
        {
            _mapper = mapper;
            _categoryBookRepository = categoryBookRepository;
        }

        [HttpPost]
        // [Authorize(Roles = "Admin")]
        [AllowAnonymous] 

        public async Task<IActionResult> Post(CategoryBookViewModel category)
        {
            var cat1 = _mapper.Map<CategoryBook>(category);

            var lista =  await _categoryBookRepository.GetAll();

            lista.ToList();

              foreach (var item in lista)
              {
               bool verifica = cat1.Description.Contains(item.Description,StringComparison.OrdinalIgnoreCase);
               if(verifica){
                 return BadRequest("Já existe no banco de dados");
                }
              }

                //Deixa Primeira letra Maiúscula
               cat1.Description = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(cat1.Description);

             _categoryBookRepository.Add(cat1);

             if (await _categoryBookRepository.UnitOfWork.SaveChangesAsync() > 0)
                return Created($"api/CategoryBook/{category.Id}", category);
            return BadRequest();
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        [AllowAnonymous] 
        public  async Task<IActionResult> GetAllCategories()
        {
            var categories = await  _categoryBookRepository.GetAll();
            return Ok(categories);
        }

        [HttpGet ("{CategoryId}")]
        //[Authorize(Roles = "Admin")]
        [AllowAnonymous] 
        public  IActionResult GetById(Guid CategoryId)
        {
            var entity =   _categoryBookRepository.GetbyId(CategoryId);
            return Ok(entity);
            
        }

        [HttpPut("{CategoryId}")]
        [Authorize(Roles = "Admin")]
        //[AllowAnonymous] 
        public async Task<IActionResult> Put(Guid CategoryId,CategoryBookViewModel category)
        {
             var entity =  _categoryBookRepository.GetbyId(CategoryId);

            if (entity == null) return NotFound();
            _mapper.Map(category, entity);
            
             //Deixa Primeira letra Maiúscula
            entity.Description = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(entity.Description);

            _categoryBookRepository.Update(entity);

            if(await _categoryBookRepository.UnitOfWork.SaveChangesAsync() > 0)
          
            return Created($"api/CategoryBook/{category.Id}", _mapper.Map<CategoryBookViewModel>(entity));
            return BadRequest();
        }

        [HttpDelete ("{CategoryId}")]
        [Authorize(Roles = "Admin")]
        //[AllowAnonymous] 
        public async Task<IActionResult> Delete(Guid CategoryId)
        {
              var entity =  _categoryBookRepository.GetbyId(CategoryId);

            if (entity == null) return NotFound();
            _categoryBookRepository.Delete(entity);

            if (await _categoryBookRepository.UnitOfWork.SaveChangesAsync() > 0)
            return Ok();
            return BadRequest(); 

    }
}
}