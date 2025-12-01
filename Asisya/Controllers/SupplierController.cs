using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Asisya.Data.Suppliers;
using Asisya.Models;
using Asisya.Dtos.SupplierDtos;
using Asisya.Middleware;
using System.Net;

namespace Asisya.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SupplierController : ControllerBase
{
    private readonly ISupplierRepository _repo;
    private readonly IMapper _mapper;

    public SupplierController(ISupplierRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SupplierResponseDto>>> GetAll()
    {
        var suppliers = await _repo.GetAll();
        return Ok(_mapper.Map<IEnumerable<SupplierResponseDto>>(suppliers));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SupplierResponseDto>> GetById(int id)
    {
        var supplier = await _repo.GetById(id);

        if (supplier is null)
            throw new MiddlewareException(HttpStatusCode.NotFound, new { mensaje = "Proveedor no encontrado" });

        return Ok(_mapper.Map<SupplierResponseDto>(supplier));
    }

    [HttpPost]
    public async Task<ActionResult<SupplierResponseDto>> Create(SupplierRequestDto dto)
    {
        var model = _mapper.Map<Supplier>(dto);
        await _repo.Create(model);
        await _repo.SaveChanges();

        var response = _mapper.Map<SupplierResponseDto>(model);

        return CreatedAtAction(nameof(GetById), new { id = model.SupplierID }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, SupplierRequestDto dto)
    {
        var supplier = await _repo.GetById(id);

        if (supplier is null)
            throw new MiddlewareException(HttpStatusCode.NotFound, new { mensaje = "Proveedor no encontrado" });

        _mapper.Map(dto, supplier);
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
