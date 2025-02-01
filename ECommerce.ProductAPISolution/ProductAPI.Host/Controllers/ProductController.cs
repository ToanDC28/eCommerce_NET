using Microsoft.AspNetCore.Mvc;
using ProductAPI.Application.DTOs;
using ProductAPI.Application.DTOs.Convertions;
using ProductAPI.Application.Interfaces;
using System.Reflection.Metadata.Ecma335;

namespace ProductAPI.Host.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProduct()
    {
        var products = await _productRepository.GetAllAsync();
        var (_, list) = ProductConvertion.FromEntity(null, products);
        return list.Any() ? Ok(list) : NotFound("No Product found");
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDTO>> GetProduct(int id)
    {
        var products = await _productRepository.GetByID(id);
        var (product, list) = ProductConvertion.FromEntity(products, null);
        return product is not null ? Ok(product) : NotFound("No Product found");
    }

    [HttpPost]
    public async Task<ActionResult<ProductDTO>> CreateProduct(ProductDTO request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var entity = ProductConvertion.ToEntity(request);
        var products = await _productRepository.CreateAsync(entity);
        return products.Flag ? Ok(products) : BadRequest(products.Message);
    }

    [HttpPut]
    public async Task<ActionResult<ProductDTO>> UpdateProduct(ProductDTO request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var entity = ProductConvertion.ToEntity(request);
        var products = await _productRepository.UpdateAsync(entity);
        return products.Flag ? Ok(products) : BadRequest(products.Message);
    }
    [HttpDelete]
    public async Task<ActionResult<ProductDTO>> DeleteProduct(ProductDTO request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var entity = ProductConvertion.ToEntity(request);
        var products = await _productRepository.DeleteAsync(entity);
        return products.Flag ? Ok(products) : BadRequest(products.Message);
    }
}
