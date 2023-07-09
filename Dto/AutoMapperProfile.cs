using MyAPI.Entities;
using MyAPI.Dto;
using AutoMapper;
public class AutoMapperProfile: AutoMapper.Profile
{
  public AutoMapperProfile() {
    CreateMap < Article, ArticleDto > ().ReverseMap();
    CreateMap <Customer, CustomerDto> ().ReverseMap();
    CreateMap <Payment, PaymentDto> ().ReverseMap();
    CreateMap <Transaction, TransactionDto> ().ReverseMap();
  }
}