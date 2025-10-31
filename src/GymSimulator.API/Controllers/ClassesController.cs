using GymSimulator.Application.Abstractions;
using GymSimulator.Application.DTOs;
using GymSimulator.Application.DTOs.Common;
using GymSimulator.Domain.Entities;
using GymSimulator.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace GymSimulator.API.Controllers;

[ApiController]
[Route("api/classes")]
public class ClassesController : ControllerBase
{
    private readonly IClassService _service;

    public ClassesController(IClassService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResponse<ClassDto>>> GetAll([FromQuery]PagedRequest request,CancellationToken ct)
    {
        return Ok(await _service.GetAllAsync(request,ct));
    }

    [HttpGet("list/available")]
    public async Task<ActionResult<PagedResponse<ClassDto>>> GetAvailable([FromQuery] PagedRequest request, CancellationToken ct)
    {
        var result = await _service.GetAvailableAsync(request, ct);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClassDto>> GetById(Guid id, CancellationToken ct)
    {
        var item = await _service.GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<ClassDto>> Create([FromBody] ClassCreateDto dto, CancellationToken ct)
    {
            var created = await _service.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ClassDto>> Update(Guid id, [FromBody] ClassUpdateDto dto, CancellationToken ct)
    {
        var updated = await _service.UpdateAsync(id, dto, ct);
        if (updated is null) return NotFound();
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id, CancellationToken ct)
    {
        var removed = await _service.DeleteAsync(id, ct);
        if (!removed) return NotFound();
        return NoContent();
    }
}


