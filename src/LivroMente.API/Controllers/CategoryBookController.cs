using AutoMapper;
using LivroMente.Domain.Models.CategoryBookModel;
using LivroMente.Domain.ViewModel;
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

        public async Task<IActionResult> Post(CategoryBookViewModel category)
        {
            var cat1 = _mapper.Map<CategoryBook>(category);
             _categoryBookRepository.Add(cat1);

             if (await _categoryBookRepository.UnitOfWork.SaveChangesAsync() > 0)
                return Created($"api/CategoryBook/{category.Id}", category);
            return BadRequest();
        }

        [HttpGet]
        public  async Task<IActionResult> GetAllCategories()
        {
            var categories = await  _categoryBookRepository.GetAll();
            return Ok(categories);
        }

        [HttpGet ("{CategoryId}")]
        public  IActionResult GetById(Guid CategoryId)
        {
            var entity =   _categoryBookRepository.GetbyId(CategoryId);
            return Ok(entity);
            
        }

        [HttpPut("{CategoryId}")]
        public async Task<IActionResult> Put(Guid CategoryId,CategoryBookViewModel category)
        {
             var entity =  _categoryBookRepository.GetbyId(CategoryId);

            if (entity == null) return NotFound();
            _mapper.Map(category, entity);
            _categoryBookRepository.Update(entity);

            if(await _categoryBookRepository.UnitOfWork.SaveChangesAsync() > 0)
          
            return Created($"api/CategoryBook/{category.Id}", _mapper.Map<CategoryBookViewModel>(entity));
            return BadRequest();
        }

        [HttpDelete ("{CategoryId}")]
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