
using System.ComponentModel.DataAnnotations;

namespace MyAPI.Entities;
public enum PaymentStatus
{
    Pending,
    Processed,
    Failed
}

public class Payment {
    public int PaymentId {get;set;}
    [Required]
    public decimal Amount {get;set;}
    public int CustomerId {get;set;}

    [Required]
    public PaymentStatus Status {get;set;}
}