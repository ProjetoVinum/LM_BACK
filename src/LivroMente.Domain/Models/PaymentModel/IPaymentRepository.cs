using LivroMente.Domain.Core.Data;

namespace LivroMente.Domain.Models.PaymentModel
{
    public interface IPaymentRepository : IRepository<Payment,Guid>
    {
         
    }
}