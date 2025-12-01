using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Asisya.Data.Customers;
using Asisya.Models;
using Asisya.Dtos.CustomerDtos;
using Asisya.Middleware;
using System.Net;

namespace Asisya.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerRepository _repo;
    private readonly IMapper _mapper;

    public CustomerController(ICustomerRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerResponseDto>>> GetAll()
    {
        var customers = await _repo.GetAll();
        return Ok(_mapper.Map<IEnumerable<CustomerResponseDto>>(customers));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerResponseDto>> GetById(string id)
    {
        var customer = await _repo.GetById(id);

        if (customer is null)
            throw new MiddlewareException(HttpStatusCode.NotFound, new { mensaje = "Cliente no encontrado" });

        return Ok(_mapper.Map<CustomerResponseDto>(customer));
    }

    [HttpPost]
    public async Task<ActionResult<CustomerResponseDto>> Create(CustomerRequestDto dto)
    {
        var model = _mapper.Map<Customer>(dto);
        await _repo.Create(model);
        await _repo.SaveChanges();

        var response = _mapper.Map<CustomerResponseDto>(model);

        return CreatedAtAction(nameof(GetById), new { id = model.CustomerID }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(string id, CustomerRequestDto dto)
    {
        var customer = await _repo.GetById(id);

        if (customer is null)
            throw new MiddlewareException(HttpStatusCode.NotFound, new { mensaje = "Cliente no encontrado" });

        _mapper.Map(dto, customer);
        await _repo.SaveChanges();

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        await _repo.Delete(id);
        await _repo.SaveChanges();
        return Ok();
    }
}
