using Microsoft.AspNetCore.Mvc;
using ASPNetExapp.Models;
using ASPNetExapp.Services;

namespace WorkersApi.Controllers;
[ApiController]
[Route("api/workers")]
public class WorkerController : ControllerBase
{
    private readonly WorkerService _workerService;

    public WorkerController(WorkerService workerService)
    {
        _workerService = workerService;
    }

    [HttpGet]
    public ActionResult<PaginatedResult<Worker>> GetWorkers(
        [FromQuery] string? query,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 2)
    {
        return Ok(_workerService.GetAllWorkers(query, page, pageSize));
    }

    [HttpGet("{id}")]
    public ActionResult<Worker> GetWorkerById(int id)
    {
        var worker = _workerService.GetWorkerById(id);
        return worker != null ? Ok(worker) : NotFound(new { message = "Worker not found" });
    }

    [HttpPost]
    public ActionResult<Worker> CreateWorker([FromBody] Worker newWorker)
    {
        _workerService.AddWorker(newWorker);
        return CreatedAtAction(nameof(GetWorkerById), new { id = newWorker.Id }, newWorker);
    }

    [HttpPatch("{id}")]
    public ActionResult UpdateWorker(int id, [FromBody] Worker updatedWorker)
    {
        if (!_workerService.UpdateWorker(id, updatedWorker))
            return NotFound(new { message = "Worker not found" });

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteWorker(int id)
    {
        if (!_workerService.DeleteWorker(id))
            return NotFound(new { message = "Worker not found" });

        return NoContent();
    }
}

