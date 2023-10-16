using System.Globalization;
using AutoMapper;
using LivroMente.Domain.Models.PaymentModel;
using LivroMente.Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LivroMente.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository _repo;
        private readonly IMapper _mapper;

        public PaymentController(IPaymentRepository repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        
        [HttpPost]
        public async Task<IActionResult> Post(PaymentViewModel payment)
        {
            var pay1 = _mapper.Map<Payment>(payment);

            var lista =  await _repo.GetAll();

            lista.ToList();

              foreach (var item in lista)
              {
               bool verifica = pay1.Description.Contains(item.Description,StringComparison.OrdinalIgnoreCase);
               if(verifica){
                 return BadRequest("Já existe no banco de dados");
                }
              }

                //Deixa Primeira letra Maiúscula
               pay1.Description = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(pay1.Description);

             _repo.Add(pay1);

             if (await _repo.UnitOfWork.SaveChangesAsync() > 0)
                return Created($"api/Payment/{payment.Id}", payment);
            return BadRequest();
        }

        [HttpGet]
        public  async Task<IActionResult> GetAllCategories()
        {
            var payments = await  _repo.GetAll();
            return Ok(payments);
        }

        [HttpGet ("{PaymentId}")]
        public  IActionResult GetById(Guid PaymentId)
        {
            var entity =   _repo.GetbyId(PaymentId);
            return Ok(entity);
            
        }

        [HttpPut("{PaymentId}")]
        public async Task<IActionResult> Put(Guid PaymentId,PaymentViewModel payment)
        {
             var entity =  _repo.GetbyId(PaymentId);

            if (entity == null) return NotFound();
            _mapper.Map(payment, entity);
            
             //Deixa Primeira letra Maiúscula
            entity.Description = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(entity.Description);

            _repo.Update(entity);

            if(await _repo.UnitOfWork.SaveChangesAsync() > 0)
          
            return Created($"api/Payment/{payment.Id}", _mapper.Map<PaymentViewModel>(entity));
            return BadRequest();
        }

        [HttpDelete ("{PaymentId}")]
        public async Task<IActionResult> Delete(Guid PaymentId)
        {
              var entity =  _repo.GetbyId(PaymentId);

            if (entity == null) return NotFound();
            _repo.Delete(entity);

            if (await _repo.UnitOfWork.SaveChangesAsync() > 0)
            return Ok();
            return BadRequest(); 

        }

    }
}