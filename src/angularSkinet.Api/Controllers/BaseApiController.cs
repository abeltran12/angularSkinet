using angularSkinet.Api.RequestHelpers;
using angularSkinet.Core.Entities;
using angularSkinet.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace angularSkinet.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseApiController : ControllerBase
{
    protected async Task<ActionResult> CreatePagedResult<T>(IGenericRepository<T> repository,
        ISpecification<T> specification, int pageIndex, int pageSize) where T : BaseEntity
    {
        var items = await repository.GetAllAsync(specification);
        var totalItems = await repository.CountAsync(specification);
        var pagination = new Pagination<T>(pageIndex, pageSize, totalItems, items);
        return Ok(pagination);
    }
}