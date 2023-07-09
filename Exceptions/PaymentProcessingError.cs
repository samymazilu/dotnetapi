  
namespace MyAPI.Exceptions;
  public class PaymentProcessingError : BusinessException 
{
    public PaymentProcessingError(int id) 
        : base($"Payment with id {id} was already processed")
    {

    }
}
    