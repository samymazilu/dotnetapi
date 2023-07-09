using Microsoft.AspNetCore.Mvc;
using MyAPI.Services;
using MyAPI.Entities;
using MyAPI.Dto;
using AutoMapper;

namespace MyAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : MyApiBaseController<Customer, CustomerDto, ICustomerRepository>
{

    public CustomerController(ICustomerRepository repository, MyDbContext dbContext, IMapper mapper) : base(repository, dbContext, mapper)
    {

    }

}