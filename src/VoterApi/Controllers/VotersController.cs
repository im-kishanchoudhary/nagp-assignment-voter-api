using Microsoft.AspNetCore.Mvc;
using VoterApi.Models;
using VoterApi.Services;

namespace VoterApi.Controllers;

[ApiController]
[Route("api/voters")]
[Produces("application/json")]
public class VotersController : ControllerBase
{
    private readonly IVoterService _service;

    public VotersController(IVoterService service)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<VoterResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var voters = await _service.GetAllAsync(ct);
        return Ok(voters);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(VoterResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var voter = await _service.GetByIdAsync(id, ct);
        return voter is null ? NotFound(new { message = $"Voter {id} was not found." }) : Ok(voter);
    }

    [HttpPost]
    [ProducesResponseType(typeof(VoterResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] VoterRequest request, CancellationToken ct)
    {
        try
        {
            var created = await _service.CreateAsync(request, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (DuplicateVoterException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(VoterResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] VoterRequest request, CancellationToken ct)
    {
        var updated = await _service.UpdateAsync(id, request, ct);
        return updated is null ? NotFound(new { message = $"Voter {id} was not found." }) : Ok(updated);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var removed = await _service.DeleteAsync(id, ct);
        return removed ? NoContent() : NotFound(new { message = $"Voter {id} was not found." });
    }
}
