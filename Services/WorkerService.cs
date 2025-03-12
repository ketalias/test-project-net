using ASPNetExapp.Models;

namespace ASPNetExapp.Services;

public class WorkerService
{
    private readonly List<Worker> _workers = new(){
        new Worker{Id = 1, LastName = "Ivanich",RoomNumber = 15, Department = "Backend", ComputerInfo= "Asus Vivobook" },
        new Worker{Id = 2, LastName = "Fofich",RoomNumber = 15, Department = "Frontkend", ComputerInfo= "Asus Pro Vivobook" },
        new Worker{Id = 3, LastName = "Bazzich",RoomNumber = 15, Department = "Ui", ComputerInfo= "Macbook" },
        new Worker{Id = 4, LastName = "Gedich",RoomNumber = 15, Department = "Ux", ComputerInfo= "Asus Vivobook" },

    };

    public Worker? GetWorkerById(int id) => _workers.FirstOrDefault(u => u.Id == id);

    public PaginatedResult<Worker> GetAllWorkers(string? query, int page, int pageSize)
    {
        var filteredWorkers = _workers
            .Where(w => string.IsNullOrEmpty(query) ||
                        w.LastName.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                        w.Department.Contains(query, StringComparison.OrdinalIgnoreCase))
            .ToList();

        var totalRecords = filteredWorkers.Count;

        var workersPage = filteredWorkers
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PaginatedResult<Worker>
        {
            Data = workersPage,
            TotalRecords = totalRecords
        };
    }
    
    public void AddWorker(Worker worker)
    {
        worker.Id = _workers.Any() ? _workers.Max(w => w.Id) + 1 : 1;
        _workers.Add(worker);
    }

    public bool UpdateWorker(int id, Worker updatedWorker)
    {
        var worker = _workers.FirstOrDefault(w => w.Id == id);
        if (worker == null) return false;
        
        worker.LastName = updatedWorker.LastName;
        worker.RoomNumber = updatedWorker.RoomNumber;
        worker.Department = updatedWorker.Department;
        worker.ComputerInfo = updatedWorker.ComputerInfo;
        
        return true;
    }

    public bool DeleteWorker(int id)
    {
        var worker = _workers.FirstOrDefault(w => w.Id == id);
        if (worker == null) return false;
        
        _workers.Remove(worker);
        return true;
    }
}