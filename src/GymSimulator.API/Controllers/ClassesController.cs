using GymSimulator.Application.Abstractions;
using GymSimulator.Application.DTOs;
using GymSimulator.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GymSimulator.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClassesController : ControllerBase
{
    private readonly IClassService _service;

    public ClassesController(IClassService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Class>>> GetAll(CancellationToken ct)
    {
        return Ok(await _service.GetAllAsync(ct));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Class>> GetById(Guid id, CancellationToken ct)
    {
        var item = await _service.GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Class>> Create([FromBody] ClassCreateDto dto, CancellationToken ct)
    {
        var exists = (await _service.GetAllAsync(ct)).Any(c => c.ClassTypeId == dto.ClassTypeId && c.ScheduledAt == dto.ScheduledAt);
        if (exists) return Conflict(new { message = "Já existe uma aula para o mesmo tipo e horário" });
        var created = await _service.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Class>> Update(Guid id, [FromBody] ClassUpdateDto dto, CancellationToken ct)
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


