
namespace MyAPI.Entities;
public enum TransactionStatus
{
    Pending,
    Processed,
    Failed
}
public class Transaction {
    public int TransactionId {get;set;}
    public List<Payment> Payments {get;set;}
    public List<Article> Articles {get;set;}
    public Customer Customer {get;set;}
    public int CustomerId {get;set;}
    public TransactionStatus Status {get;set;}

}