using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Asisya.Data.Shippers;
using Asisya.Models;
using Asisya.Dtos.ShipperDtos;
using Asisya.Middleware;
using System.Net;

namespace Asisya.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShipperController : ControllerBase
{
    private readonly IShipperRepository _repo;
    private readonly IMapper _mapper;

    public ShipperController(IShipperRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShipperResponseDto>>> GetAll()
    {
        var shippers = await _repo.GetAll();
        return Ok(_mapper.Map<IEnumerable<ShipperResponseDto>>(shippers));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ShipperResponseDto>> GetById(int id)
    {
        var shipper = await _repo.GetById(id);

        if (shipper is null)
            throw new MiddlewareException(HttpStatusCode.NotFound, new { mensaje = "Shipper no encontrado" });

        return Ok(_mapper.Map<ShipperResponseDto>(shipper));
    }

    [HttpPost]
    public async Task<ActionResult<ShipperResponseDto>> Create(ShipperRequestDto dto)
    {
        var model = _mapper.Map<Shipper>(dto);
        await _repo.Create(model);
        await _repo.SaveChanges();

        var response = _mapper.Map<ShipperResponseDto>(model);

        return CreatedAtAction(nameof(GetById), new { id = model.ShipperID }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, ShipperRequestDto dto)
    {
        var shipper = await _repo.GetById(id);

        if (shipper is null)
            throw new MiddlewareException(HttpStatusCode.NotFound, new { mensaje = "Shipper no encontrado" });

        _mapper.Map(dto, shipper);
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
