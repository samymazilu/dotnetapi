using MyAPI.Entities;
namespace MyAPI.Services;
public class PaymentRepository : Repository<Payment>, IPaymentRepository{
        public PaymentRepository(MyDbContext context):base(context){
            
        }

}