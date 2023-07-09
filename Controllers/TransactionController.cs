using Microsoft.AspNetCore.Mvc;
using MyAPI.Services;
using MyAPI.Entities;
using MyAPI.Exceptions;
using AutoMapper;
using MyAPI.Dto;

namespace MyAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionController : MyApiBaseController<Transaction, TransactionDto, ITransactionRepository>
{

    private readonly ITransactionRepository _transactionRepository;

    private readonly MyDbContext _dbContext;
    private readonly ITransactionService _transactionService;
    private readonly IMapper _mappper;
    public TransactionController(ITransactionRepository transactionRepository, ITransactionService transactionService, MyDbContext dbContext, IMapper mapper) : base(transactionRepository, dbContext, mapper)
    {
        _transactionRepository = transactionRepository;
        _dbContext = dbContext;
        _transactionService = transactionService;
        _mappper = mapper;
    }


    public override async Task<ActionResult<Transaction>> PostItem(TransactionDto transaction)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var entity = _mappper.Map<Transaction>(transaction);    
        try
        {
            await _transactionService.ProcessTransaction(entity);
        }
        catch (BusinessException ex)
        {
            return UnprocessableEntity(ex.Message);
        }
        await _dbContext.SaveChangesAsync();

        return Ok();
    }


}