using GymSimulator.Application.Abstractions;
using GymSimulator.Application.DTOs;
using GymSimulator.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GymSimulator.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlanTypesController : ControllerBase
{
    private readonly IPlanTypeService _service;

    public PlanTypesController(IPlanTypeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlanType>>> GetAll(CancellationToken ct)
    {
        return Ok(await _service.GetAllAsync(ct));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PlanType>> GetById(Guid id, CancellationToken ct)
    {
        var item = await _service.GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<PlanType>> Create([FromBody] PlanTypeCreateDto dto, CancellationToken ct)
    {
        // regra básica anti-duplicação por Name
        var exists = (await _service.GetAllAsync(ct)).Any(x => x.Name == dto.Name);
        if (exists) return Conflict(new { message = "Tipo de plano já existe com este nome" });
        var created = await _service.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PlanType>> Update(Guid id, [FromBody] PlanTypeUpdateDto dto, CancellationToken ct)
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


