using GymSimulator.Application.Abstractions;
using GymSimulator.Application.DTOs;
using GymSimulator.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GymSimulator.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _service;

    public BookingsController(IBookingService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Booking>>> GetAll(CancellationToken ct)
    {
        return Ok(await _service.GetAllAsync(ct));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Booking>> GetById(Guid id, CancellationToken ct)
    {
        var item = await _service.GetByIdAsync(id, ct);
        if (item is null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<Booking>> Create([FromBody] BookingCreateDto dto, CancellationToken ct)
    {
        var already = (await _service.GetAllAsync(ct)).Any(b => b.StudentId == dto.StudentId && b.ClassId == dto.ClassId);
        if (already) return Conflict(new { message = "Reserva já existe para este aluno e aula" });
        var created = await _service.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPost("{id}/cancel")]
    public async Task<ActionResult> Cancel(Guid id, [FromQuery] string? reason, CancellationToken ct)
    {
        var ok = await _service.CancelAsync(id, reason, ct);
        if (!ok) return NotFound();
        return NoContent();
    }
}


