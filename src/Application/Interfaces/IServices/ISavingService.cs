using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.IServices;

public interface ISavingService
{
    Task<Saving> GetSavingByIdAsync(int id);
    Task<Saving> GetSavingByBudgetIdAsync(int id);
    Task<List<Saving>> GetAllSavingsAsync();
    Task CreateSavingAsync(Saving saving);
    Task EditSavingAsync(int id, decimal value, decimal aimValue);
    Task DeleteSavingAsync(int id);
    Task SavingSupplyMoney(IFormCollection form, decimal value);
}