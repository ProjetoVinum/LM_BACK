using LivroMente.Domain.Core.Data;

namespace LivroMente.Domain.Models.OrderModel
{
    public interface IOrderRepository : IRepository<Order,Guid>
    {
         List<LivroMente.Domain.Models.OrderModel.Order> GetByIdOrders(Guid id);
    }
}