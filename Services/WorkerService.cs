using ASPNetExapp.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNetExapp.Services;

public class WorkerService
{
    private readonly AppDbContext _context;

    public WorkerService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Worker?> GetWorkerById(int id)
    {
        return await _context.Workers.FindAsync(id);
    }

    public async Task<PaginatedResult<Worker>> GetAllWorkers(string? query, int page, int pageSize)
    {
        var workersQuery = _context.Workers.AsQueryable();

        if (!string.IsNullOrEmpty(query))
        {
            workersQuery = workersQuery.Where(w => 
                EF.Functions.ILike(w.LastName, $"%{query}%") || 
                EF.Functions.ILike(w.Department, $"%{query}%"));
        }

        var totalRecords = await workersQuery.CountAsync();
        var workersPage = await workersQuery
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedResult<Worker>
        {
            Data = workersPage,
            TotalRecords = totalRecords
        };
    }

    public async Task AddWorker(Worker worker)
    {
        _context.Workers.Add(worker);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateWorker(int id, Worker updatedWorker)
    {
        var worker = await _context.Workers.FindAsync(id);
        if (worker == null) return false;

        worker.LastName = updatedWorker.LastName;
        worker.RoomNumber = updatedWorker.RoomNumber;
        worker.Department = updatedWorker.Department;
        worker.ComputerInfo = updatedWorker.ComputerInfo;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteWorker(int id)
    {
        var worker = await _context.Workers.FindAsync(id);
        if (worker == null) return false;

        _context.Workers.Remove(worker);
        await _context.SaveChangesAsync();
        return true;
    }
}