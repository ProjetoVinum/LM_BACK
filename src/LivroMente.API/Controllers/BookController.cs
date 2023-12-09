using System.Globalization;
using System.Net.Http.Headers;
using AutoMapper;
using LivroMente.Domain.Models.BookModel;
using LivroMente.Domain.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LivroMente.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        

        public BookController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpPost]
      //  [Authorize(Roles = "Admin")]
       [AllowAnonymous]
        public async Task<IActionResult> Post(BookViewModel book)
        {
            var book1 = _mapper.Map<Book>(book);

            // Deixa primeira letra maiúscula
            book1.Author = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(book1.Author); 

            _bookRepository.Add(book1);

           if (await _bookRepository.UnitOfWork.SaveChangesAsync() > 0)
              return Created($"api/Book/{book.Id}", book);
            return BadRequest();
        }
        [HttpGet]
        [AllowAnonymous]
        public  async Task<IActionResult> GetAllBook()
        {
            var book = await  _bookRepository.GetAll();
            return Ok(book);
        }

        [HttpGet ("{BookId}")]
        [AllowAnonymous]
        public  IActionResult GetById(Guid BookId)
        {
            var entity =   _bookRepository.GetbyId(BookId);
            return Ok(entity);
            
        }

        [HttpPut("{BookId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(Guid BookId, BookViewModel book)
        {
             var entity =  _bookRepository.GetbyId(BookId);

            if (entity == null) return NotFound();
            _mapper.Map(book, entity);
            
             //Deixa Primeira letra Maiúscula
           // entity.Description = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(entity.Description);

            _bookRepository.Update(entity);

            if(await _bookRepository.UnitOfWork.SaveChangesAsync() > 0)
                return Created($"api/Book/{book.Id}", _mapper.Map<BookViewModel>(entity));
            return BadRequest();
        }

        [HttpDelete ("{BookId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid BookId)
        {
              var entity =  _bookRepository.GetbyId(BookId);

            if (entity == null) return NotFound();
            _bookRepository.Delete(entity);

            if (await _bookRepository.UnitOfWork.SaveChangesAsync() > 0)
            return Ok();
            return BadRequest(); 

        }
    }
}