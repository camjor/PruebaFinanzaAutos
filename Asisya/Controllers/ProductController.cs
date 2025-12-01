using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Asisya.Data.Products;
using Asisya.Models;
using Asisya.Dtos.ProductDtos;
using Asisya.Middleware;
using System.Net;

namespace Asisya.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _repo;
    private readonly IMapper _mapper;

    public ProductController(IProductRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductResponseDto>>> GetAll()
    {
        var products = await _repo.GetAll();
        return Ok(_mapper.Map<IEnumerable<ProductResponseDto>>(products));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponseDto>> GetById(int id)
    {
        var product = await _repo.GetById(id);

        if (product is null)
            throw new MiddlewareException(HttpStatusCode.NotFound, new { mensaje = "Producto no encontrado" });

        return Ok(_mapper.Map<ProductResponseDto>(product));
    }

    [HttpPost]
    public async Task<ActionResult<ProductResponseDto>> Create(ProductRequestDto dto)
    {
        var model = _mapper.Map<Product>(dto);
        await _repo.Create(model);
        await _repo.SaveChanges();

        var response = _mapper.Map<ProductResponseDto>(model);

        return CreatedAtAction(nameof(GetById), new { id = model.ProductID }, response);
    }

    // ---------- CARGA MASIVA ----------
    [HttpPost("bulk")]
    public async Task<ActionResult> BulkCreate([FromQuery] int count, [FromQuery] int? categoryId, [FromQuery] int? supplierId)
    {
        await _repo.BulkCreate(count, categoryId, supplierId);
        return Ok(new { mensaje = $"{count} productos generados correctamente" });
    }
    // -----------------------------------

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, ProductRequestDto dto)
    {
        var product = await _repo.GetById(id);
        if (product is null)
            throw new MiddlewareException(HttpStatusCode.NotFound, new { mensaje = "Producto no encontrado" });

        _mapper.Map(dto, product);
        await _repo.SaveChanges();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _repo.Delete(id);
        await _repo.SaveChanges();
        return Ok();
    }
}
