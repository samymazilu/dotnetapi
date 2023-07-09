using Microsoft.AspNetCore.Mvc;
using MyAPI.Services;
using MyAPI.Entities;
using MyAPI.Dto;
using AutoMapper;

namespace MyAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ArticleController : MyApiBaseController<Article, ArticleDto, IArticleRepository>
{

    public ArticleController(IArticleRepository repository, MyDbContext dbContext, IMapper mapper) : base(repository, dbContext, mapper)
    {

    }

}