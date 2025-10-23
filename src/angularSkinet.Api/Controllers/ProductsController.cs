using angularSkinet.Core.Entities;
using angularSkinet.Core.Interfaces;
using angularSkinet.Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace angularSkinet.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IGenericRepository<Product> repository) : ControllerBase
{
    private readonly IGenericRepository<Product> _repository = repository;

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> 
        GetProducts(string? brand, string? type, string? sort)
    {
        var spec = new ProductSpecification(brand, type, sort);
        var products = await _repository.GetAllAsync(spec);
        return Ok(products);
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetBrands()
    {
        var spec = new BrandListSpecification();
        var brands = await _repository.GetAllAsync(spec);
        return Ok(brands);
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetTypes()
    {
        var spec = new TypeListSpecification();
        var types = await _repository.GetAllAsync(spec);
        return Ok(types);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product == null) return NotFound();

        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        _repository.Add(product);
        if (!await _repository.SaveAllAsync())
        {   
            return BadRequest("Problem creating product");
        }

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, [FromBody] Product product)
    {
        if (!ProductExists(id) || id != product.Id)
            return BadRequest("Cannot update this product");

        _repository.Update(product);
        if (await _repository.SaveAllAsync())
            return NoContent();

        return BadRequest("Problem updating the product");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var productExists = await _repository.GetByIdAsync(id);

        if (productExists == null) return NotFound();

        _repository.Delete(productExists);
        if (await _repository.SaveAllAsync())
            return NoContent();

        return BadRequest("Problem deleting the product");
    }

    private bool ProductExists(int id) => _repository.Exists(id);
}
