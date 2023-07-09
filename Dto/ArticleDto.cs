using System.ComponentModel.DataAnnotations;
namespace MyAPI.Dto;
public class ArticleDto{
    public string ArticleId {get;set;}
    [Required]
    [StringLength(100)]
    public string ArticleName {get;set;}

    [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a positive number")]
    public int Quantity {get;set;}
      [Range(0, int.MaxValue, ErrorMessage = "Price must be a positive number")]
    public Decimal Price {get;set;}
}