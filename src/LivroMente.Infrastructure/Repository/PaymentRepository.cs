using LivroMente.Domain.Models.PaymentModel;
using LivroMente.Infrastructure.Data;

namespace LivroMente.Infrastructure.Repository
{
    public class PaymentRepository : RepositoryBase<Payment, Guid>, IPaymentRepository
    {
          public PaymentRepository(ApplicationDataContext applicationDataContext) : base(applicationDataContext)
          {
            
          }
    }
}