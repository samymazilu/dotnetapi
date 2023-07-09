  namespace MyAPI.Exceptions;
  public class ArticleNotInStockException : BusinessException
{
    public ArticleNotInStockException(int id) 
        : base($"Item with id {id} is not in stock anymore.")
    {

    }
}
    