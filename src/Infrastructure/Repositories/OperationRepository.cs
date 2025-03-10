using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class OperationRepository : IOperationRepository
{
    private readonly DataContext _context;

    public OperationRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Operation> GetOperationByIdAsync(int id)
    {
        var operation = await _context.Operations
                               .AsNoTracking()
                               .FirstOrDefaultAsync(o => o.Id == id);

        return operation;
    }

    public async Task<List<Operation>> GetAllOperationsAsync(string userId)
    {
        var operations = await _context.Operations
                                .Where(o => o.UserId == userId)
                                .AsNoTracking()
                                .ToListAsync();

        return operations;
    }

    public async Task CreateOperationAsync(Operation operation)
    {
        await _context.Operations.AddAsync(operation);
        await _context.SaveChangesAsync();
    }

    public async Task EditOperationAsync(Operation actualOperation, Operation operation, string userId)
    {
        await _context.Operations
               .Where(o => o.Id == operation.Id)
               .ExecuteUpdateAsync(p => p
               .SetProperty(x => x.UserId, userId)
               .SetProperty(x => x.Value, actualOperation.Value)
               .SetProperty(x => x.Month, actualOperation.Month)
               .SetProperty(x => x.Description, actualOperation.Description)
               .SetProperty(x => x.BudgetId, actualOperation.BudgetId)
               .SetProperty(x => x.CategoryId, actualOperation.CategoryId)
               .SetProperty(x => x.EditDate, DateTime.Now));
    }

    public async Task DeleteOperationAsync(Operation operation)
    {
        _context.Operations.Remove(operation);
        await _context.SaveChangesAsync();
    }
}