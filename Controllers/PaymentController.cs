using Microsoft.AspNetCore.Mvc;
using MyAPI.Services;
using MyAPI.Entities;
using MyAPI.Dto;
using AutoMapper;

namespace MyAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentController : MyApiBaseController<Payment, PaymentDto, IPaymentRepository>
{

    public PaymentController(IPaymentRepository repository, MyDbContext dbContext, IMapper mapper) : base(repository, dbContext, mapper){

    }
    
}