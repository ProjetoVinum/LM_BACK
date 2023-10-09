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

        public CategoryBookController(ICategoryBookRepository categoryBookRepository)
        {
            _categoryBookRepository = categoryBookRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CategoryBookViewModel request)
        {
            // Map ViewModel to Domain Model
            var category = new CategoryBook
            {
                Id= Guid.NewGuid(),
                Description= request.Description,
                IsFree= request.IsFree,
            };

            _categoryBookRepository.Add(category);
            // Domain model to ViewModel
            var response = new CategoryBookViewModel
            {
                Id = category.Id,
                Description = category.Description,
                IsFree= category.IsFree
            };
            
            if(await _categoryBookRepository.UnitOfWork.SaveChangesAsync() > 0){
                 return Created($"api/CategoryBook/{category.Id}", category);
            }
            

            return BadRequest();
        }

        // GET: https://localhost:7107/api/Category
        [HttpGet]
        public  async Task<IActionResult> GetAllCategories()
        {
            var categories = await  _categoryBookRepository.GetAll();

            // Map Domain model to DTO

            //var response = new List<CategoryDto>();
            //foreach (var category in caterogies)
            //{
            //    response.Add(new CategoryDto
            //    {
            //        Id = category.Id,
            //        Name = category.Name,
            //        UrlHandle = category.UrlHandle
            //    });
            //}

            return Ok(categories);
        }

        [HttpGet ("{CategoryId}")]
        public  IActionResult GetById(Guid CategoryId)
        {
            var entity =   _categoryBookRepository.GetbyId(CategoryId);
            return Ok(entity);
            
        }

        [HttpPut("{CategoryId}")]
        public async Task<IActionResult> Put(Guid CategoryId,[FromBody] CategoryBookViewModel request)
        {
             var entity =   _categoryBookRepository.GetbyId(CategoryId);

             if (entity == null) return NotFound();

             var category = new  CategoryBookViewModel
             {
                Id = entity.Id,
                Description= request.Description,
                IsFree= request.IsFree,
             }; 
              
              _categoryBookRepository.Update(entity);

            if(await _categoryBookRepository.UnitOfWork.SaveChangesAsync() > 0)
            return Created($"api/CategoryBook/{category.Id}", category);

            return BadRequest();
        }

        [HttpDelete ("{CategoryId}")]
        public async Task<IActionResult> Delete(Guid CategoryId)
        {
            var entity = _categoryBookRepository.GetbyId(CategoryId);
           
             if (entity == null) return NotFound();
            _categoryBookRepository.Delete(entity);

          var result =  await _categoryBookRepository.UnitOfWork.SaveChangesAsync() > 0;

          if(result)
            return Ok();
            
            return BadRequest();  
        }

    }
}