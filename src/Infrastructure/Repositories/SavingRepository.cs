using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SavingRepository : ISavingRepository
{
    private readonly DataContext _context;

    public SavingRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Saving> GetSavingByIdAsync(int id)
    {
        var saving = await _context.Savings
                            .AsNoTracking()
                            .FirstOrDefaultAsync(s => s.Id == id);

        return saving;
    }

    public async Task<Saving> GetSavingByBudgetIdAsync(int id)
    {
        var saving = await _context.Savings
                            .AsNoTracking()
                            .FirstOrDefaultAsync(s => s.BudgetId == id);

        return saving;
    }

    public async Task<List<Saving>> GetAllSavingsAsync(string userId)
    {
        var savings = await _context.Savings
                             .Where(s => s.UserId == userId)
                             .AsNoTracking()
                             .ToListAsync();

        return savings;
    }

    public async Task CreateSavingAsync(Saving saving)
    {
        await _context.Savings.AddAsync(saving);
        await _context.SaveChangesAsync();
    }

    public async Task EditSavingAsync(Saving saving)
    {
        await _context.Savings
               .Where(s => s.Id == saving.Id)
               .ExecuteUpdateAsync(p => p
               .SetProperty(x => x.Name, saving.Name)
               .SetProperty(x => x.Description, saving.Description)
               .SetProperty(x => x.AimValue, saving.AimValue)
               .SetProperty(x => x.Value, saving.Value));
    }

    public async Task DeleteSavingAsync(Saving saving)
    {
        await _context.Savings
               .Where(s => s.Id == saving.Id)
               .ExecuteDeleteAsync();
    }
}