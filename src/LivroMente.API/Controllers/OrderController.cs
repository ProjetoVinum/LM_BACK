using AutoMapper;
using LivroMente.Domain.Models.OrderModel;
using LivroMente.Domain.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LivroMente.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository,IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        [AllowAnonymous] 
        public async Task<IActionResult> Post(OrderViewModel order)
        {
            var pay1 = _mapper.Map<Order>(order);

            

                //Deixa Primeira letra Maiúscula
               //pay1.Description = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(pay1.Description);
               //pay1.OrderDetails

             _orderRepository.Add(pay1);

             if (await _orderRepository.UnitOfWork.SaveChangesAsync() > 0)
                return Created($"api/Order/{order.Id}", order);
            return BadRequest();
        }

        [HttpGet ("{OrderId}")]
        //[Authorize(Roles = "Admin")]
        [AllowAnonymous] 
        public  IActionResult GetById(Guid OrderId)
        {
            var entity =   _orderRepository.GetByIdOrders(OrderId);
            return Ok(entity);
            
        }

    }
}