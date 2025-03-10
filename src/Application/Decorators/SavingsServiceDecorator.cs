using Application.Interfaces.IServices;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Application.Decorators;

public class SavingsServiceDecorator : ISavingService
{
    private readonly ISavingService _savingService;
    private readonly ILogger _logger;

    public SavingsServiceDecorator(ISavingService savingService, ILogger logger)
    {
        _savingService = savingService;
        _logger = logger;
    }

    public async Task CreateSavingAsync(Saving saving)
    {
        await _savingService.CreateSavingAsync(saving);

        _logger.LogInformation("Created saving {saving}", JsonSerializer.Serialize(saving));
    }

    public async Task DeleteSavingAsync(int id)
    {
        await _savingService.DeleteSavingAsync(id);

        _logger.LogInformation("Deleted saving with Id {savingId}", id);
    }

    public async Task EditSavingAsync(int id, decimal value, decimal aimValue)
    {
        await _savingService.EditSavingAsync(id, value, aimValue);

        _logger.LogInformation("Edited saving with Id {savingId}", id);
    }

    public async Task<List<Saving>> GetAllSavingsAsync()
    {
        var savings = await _savingService.GetAllSavingsAsync();

        _logger.LogInformation("Savings fetched: {savings}", savings.Count);
        savings.ForEach(saving => _logger.LogInformation("Saving fetched: {account}", JsonSerializer.Serialize(saving)));

        return savings;
    }

    public async Task<Saving> GetSavingByBudgetIdAsync(int id)
    {
        var saving = await _savingService.GetSavingByIdAsync(id);

        _logger.LogInformation("Saving fetched: {saving}", JsonSerializer.Serialize(saving));

        return saving;
    }

    public async Task<Saving> GetSavingByIdAsync(int id)
    {
        var saving = await _savingService.GetSavingByIdAsync(id);

        _logger.LogInformation("Saving fetched: {saving}", JsonSerializer.Serialize(saving));

        return saving;
    }

    public async Task SavingSupplyMoney(IFormCollection form, decimal value)
    {
        await _savingService.SavingSupplyMoney(form, value);

        _logger.LogInformation("Saving supply with Id: {savingId}", form["ID"]);
    }
}
