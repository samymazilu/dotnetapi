  
namespace MyAPI.Exceptions;
  public abstract class BusinessException : Exception 
{
    public BusinessException(string message) 
        : base(message)
    {

    }
}
    