  
namespace MyAPI.Exceptions;
  public class ItemNotFoundException<T> : BusinessException where T : class
{
    public ItemNotFoundException(int id) 
        : base($"{typeof(T).FullName} item with id {id} was not found.")
    {

    }
}
    