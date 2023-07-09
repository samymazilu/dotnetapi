using MyAPI.Entities;
namespace MyAPI.Services;
public class CustomerRepository : Repository<Customer>, ICustomerRepository{
        public CustomerRepository(MyDbContext context):base(context){
            
        }

}