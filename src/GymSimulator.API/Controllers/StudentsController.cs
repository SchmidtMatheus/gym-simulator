using GymSimulator.Application.Abstractions;
using GymSimulator.Application.DTOs;
using GymSimulator.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GymSimulator.API.Controllers;

[ApiController]
[Route("api/students")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _service;

    public StudentsController(IStudentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetAll(CancellationToken ct)
    {
        var items = await _service.GetAllAsync(ct);
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Student>> GetById(Guid id, CancellationToken ct)
    {
        var item = await _service.GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Student>> Create([FromBody] StudentCreateDto dto, CancellationToken ct)
    {
        var existing = (await _service.GetAllAsync(ct))
            .FirstOrDefault(s => (!string.IsNullOrEmpty(dto.Email) && s.Email == dto.Email)
                              || (!string.IsNullOrEmpty(dto.Phone) && s.Phone == dto.Phone));
        if (existing is not null)
            return Conflict(new { message = "Aluno já existe com mesmo e-mail ou telefone", id = existing.Id });

        var created = await _service.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Student>> Update(Guid id, [FromBody] StudentUpdateDto dto, CancellationToken ct)
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


