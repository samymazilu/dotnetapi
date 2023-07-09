
using System.ComponentModel.DataAnnotations;

namespace MyAPI.Dto;
public enum PaymentStatus
{
    Pending,
    Processed,
    Failed
}

public class PaymentDto {
    public int PaymentId {get;set;}
    [Required]
    public decimal Amount {get;set;}
    public int CustomerId {get;set;}

    [Required]
    public PaymentStatus Status {get;set;}
}