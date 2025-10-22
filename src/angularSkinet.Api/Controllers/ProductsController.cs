using angularSkinet.Core.Entities;
using angularSkinet.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace angularSkinet.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IProductRepository repository) : ControllerBase
{
    private readonly IProductRepository _repository = repository;

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> 
        GetProducts(string? brand, string? type, string? sort)
    {
        var products = await _repository.GetProductsAsync(brand, type, sort);
        return Ok(products);
    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetBrands()
    {
        var brands = await _repository.GetBrandsAsync();
        return Ok(brands);
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetTypes()
    {
        var types = await _repository.GetTypesAsync();
        return Ok(types);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _repository.GetProductByIdAsync(id);

        if (product == null) return NotFound();

        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        _repository.AddProduct(product);
        if (!await _repository.SaveChangesAsync())
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

        _repository.UpdateProduct(product);
        if (await _repository.SaveChangesAsync())
            return NoContent();

        return BadRequest("Problem updating the product");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var productExists = await _repository.GetProductByIdAsync(id);

        if (productExists == null) return NotFound();

        _repository.DeleteProduct(productExists);
        if (await _repository.SaveChangesAsync())
            return NoContent();

        return BadRequest("Problem deleting the product");
    }

    private bool ProductExists(int id) => _repository.ProductExists(id);
}
