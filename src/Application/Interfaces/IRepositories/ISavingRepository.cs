using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface ISavingRepository
{
    Task<Saving> GetSavingByIdAsync(int id);
    Task<Saving> GetSavingByBudgetIdAsync(int id);
    Task<List<Saving>> GetAllSavingsAsync(string userId);
    Task CreateSavingAsync(Saving saving);
    Task EditSavingAsync(Saving saving);
    Task DeleteSavingAsync(Saving saving);
}