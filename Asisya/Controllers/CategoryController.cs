using Microsoft.AspNetCore.Mvc;
using Asisya.Data.Categories;
using Asisya.Models;
using Asisya.Middleware;
using AutoMapper;
using System.Net;
using Asisya.Dtos.CategoryDtos;

namespace Asisya.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _repo;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryResponseDto>>> GetAll()
    {
        var categories = await _repo.GetAll();
        return Ok(_mapper.Map<IEnumerable<CategoryResponseDto>>(categories));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryResponseDto>> GetById(int id)
    {
        var category = await _repo.GetById(id);

        if (category is null)
            throw new MiddlewareException(HttpStatusCode.NotFound, new { mensaje = "Categoría no encontrada" });

        return Ok(_mapper.Map<CategoryResponseDto>(category));
    }

    [HttpPost]
    public async Task<ActionResult<CategoryResponseDto>> Create(CategoryRequestDto dto)
    {
        var category = _mapper.Map<Category>(dto);
        await _repo.Create(category);
        await _repo.SaveChanges();

        var response = _mapper.Map<CategoryResponseDto>(category);
        return CreatedAtAction(nameof(GetById), new { id = category.CategoryID }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, CategoryRequestDto dto)
    {
        var category = await _repo.GetById(id);
        if (category is null)
            throw new MiddlewareException(HttpStatusCode.NotFound, new { mensaje = "Categoría no encontrada" });

        _mapper.Map(dto, category);
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
