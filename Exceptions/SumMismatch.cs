  
namespace MyAPI.Exceptions;
  public class SumMismatchError : BusinessException 
{
    public SumMismatchError(int id) 
        : base($"Transaction {id} Sum from payments not matching total articles price")
    {

    }
}
    