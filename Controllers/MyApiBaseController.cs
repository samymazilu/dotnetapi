using Microsoft.AspNetCore.Mvc;
using MyAPI.Services;
using AutoMapper;
namespace MyAPI.Controllers;

public abstract class MyApiBaseController<T, TDto, TRepository> : ControllerBase
where T : class
where TDto : class
where TRepository : IRepository<T>
{
    protected TRepository _repository;
    protected MyDbContext _myDbContext;
    private readonly IMapper _mapper;
    public MyApiBaseController(TRepository repository, MyDbContext dbContext, IMapper mapper)
    {
        this._repository = repository;
        this._myDbContext = dbContext;
        this._mapper = mapper;
    }

    [HttpGet("{id}")]
    public virtual async Task<ActionResult<T>> Get(int id)
    {
        if (id < 1) return BadRequest();
        var result = await _repository.GetByIdAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPost]
    public virtual async Task<ActionResult<T>> PostItem(TDto item)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var entity = _mapper.Map<T>(item);
        _repository.Add(entity);
        await _myDbContext.SaveChangesAsync();
        return CreatedAtAction(nameof(PostItem), item);
    }

    [HttpPut()]
    public virtual async Task<IActionResult> PutItem(T item)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var entity = _mapper.Map<T>(item);
        _repository.Update(entity);
        await _myDbContext.SaveChangesAsync();
        return Ok();
    }
    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> DeleteItem(int id)
    {
        var item = await _repository.GetByIdAsync(id);
        if (item == null)
        {
            return NotFound();
        }
        _repository.Delete(item);
        await _myDbContext.SaveChangesAsync();

        return Ok();
    }
}
