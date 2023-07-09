using MyAPI.Entities;
namespace MyAPI.Services;
public class TransactionRepository : Repository<Transaction>, ITransactionRepository{
        public TransactionRepository(MyDbContext context):base(context){
            
        }

}