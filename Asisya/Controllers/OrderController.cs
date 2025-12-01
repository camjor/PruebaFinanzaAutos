using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Asisya.Data.Orders;
using Asisya.Models;
using Asisya.Dtos.OrderDtos;
using Asisya.Middleware;
using System.Net;

namespace Asisya.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _repo;
    private readonly IMapper _mapper;

    public OrderController(IOrderRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetAll()
    {
        var orders = await _repo.GetAll();
        return Ok(_mapper.Map<IEnumerable<OrderResponseDto>>(orders));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderResponseDto>> GetById(int id)
    {
        var order = await _repo.GetById(id);

        if (order is null)
            throw new MiddlewareException(HttpStatusCode.NotFound, new { mensaje = "Orden no encontrada" });

        return Ok(_mapper.Map<OrderResponseDto>(order));
    }

    [HttpPost]
    public async Task<ActionResult<OrderResponseDto>> Create(OrderRequestDto dto)
    {
        var model = _mapper.Map<Order>(dto);
        await _repo.Create(model);
        await _repo.SaveChanges();

        var response = _mapper.Map<OrderResponseDto>(model);

        return CreatedAtAction(nameof(GetById), new { id = model.OrderID }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, OrderRequestDto dto)
    {
        var order = await _repo.GetById(id);

        if (order is null)
            throw new MiddlewareException(HttpStatusCode.NotFound, new { mensaje = "Orden no encontrada" });

        _mapper.Map(dto, order);
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
