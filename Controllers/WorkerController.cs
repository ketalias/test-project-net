using ASPNetExapp.Models;
using ASPNetExapp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASPNetExapp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkerController : ControllerBase
{
    private readonly WorkerService _workerService;

    public WorkerController(WorkerService workerService)
    {
        _workerService = workerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? query, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var result = await _workerService.GetAllWorkers(query, page, pageSize);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var worker = await _workerService.GetWorkerById(id);
        if (worker == null) return NotFound();
        return Ok(worker);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Worker worker)
    {
        await _workerService.AddWorker(worker);
        return CreatedAtAction(nameof(GetById), new { id = worker.Id }, worker);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Worker updatedWorker)
    {
        var success = await _workerService.UpdateWorker(id, updatedWorker);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _workerService.DeleteWorker(id);
        if (!success) return NotFound();
        return NoContent();
    }
}