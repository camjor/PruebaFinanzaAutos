using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Asisya.Data.Employees;
using Asisya.Models;
using Asisya.Dtos.EmployeeDtos;
using Asisya.Middleware;
using System.Net;

namespace Asisya.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeRepository _repo;
    private readonly IMapper _mapper;

    public EmployeeController(IEmployeeRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EmployeeResponseDto>>> GetAll()
    {
        var employees = await _repo.GetAll();
        return Ok(_mapper.Map<IEnumerable<EmployeeResponseDto>>(employees));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmployeeResponseDto>> GetById(int id)
    {
        var employee = await _repo.GetById(id);

        if (employee is null)
            throw new MiddlewareException(HttpStatusCode.NotFound, new { mensaje = "Empleado no encontrado" });

        return Ok(_mapper.Map<EmployeeResponseDto>(employee));
    }

    [HttpPost]
    public async Task<ActionResult<EmployeeResponseDto>> Create(EmployeeRequestDto dto)
    {
        var model = _mapper.Map<Employee>(dto);
        await _repo.Create(model);
        await _repo.SaveChanges();

        var response = _mapper.Map<EmployeeResponseDto>(model);

        return CreatedAtAction(nameof(GetById), new { id = model.EmployeeID }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, EmployeeRequestDto dto)
    {
        var employee = await _repo.GetById(id);

        if (employee is null)
            throw new MiddlewareException(HttpStatusCode.NotFound, new { mensaje = "Empleado no encontrado" });

        _mapper.Map(dto, employee);
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
