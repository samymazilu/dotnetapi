
namespace MyAPI.Dto;
public enum TransactionStatus
{
    Pending,
    Processed,
    Failed
}
public class TransactionDto {
    public int TransactionId {get;set;}
    public List<PaymentDto> Payments {get;set;}
    public List<ArticleDto> Articles {get;set;}
    public int CustomerId {get;set;}
    
    public TransactionStatus Status {get;set;}

}