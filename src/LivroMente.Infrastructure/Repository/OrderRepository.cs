using LivroMente.Domain.Models.OrderModel;
using LivroMente.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LivroMente.Infrastructure.Repository
{
    public class OrderRepository : RepositoryBase<Order, Guid>, IOrderRepository
    {
        private readonly ApplicationDataContext _applicationDataContext;

        public OrderRepository(ApplicationDataContext applicationDataContext) : base(applicationDataContext)
          {
            _applicationDataContext = applicationDataContext;
        }

        public List<Order> GetByIdOrders(Guid id)
        {
            IQueryable<Order> entity =  _applicationDataContext.Order
             .Where(b=> b.Id == id)
            .Include(p=>p.OrderDetails);
             
            
            return entity.ToList();
        }
    }
}