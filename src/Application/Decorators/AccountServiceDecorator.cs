using Application.Interfaces.IServices;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Application.Decorators;

public class AccountServiceDecorator : IAccountService
{
    private readonly IAccountService _accountService;
    private readonly ILogger<AccountServiceDecorator> _logger;

    public AccountServiceDecorator(IAccountService accountService, ILogger<AccountServiceDecorator> logger)
    {
        _accountService = accountService;
        _logger = logger;
    }

    public async Task CreateAccountAsync(Account account)
    {
        await _accountService.CreateAccountAsync(account);

        _logger.LogInformation("Created account {account}", JsonSerializer.Serialize(account));
    }

    public async Task DeleteAccountAsync(Account account)
    {
        await _accountService.DeleteAccountAsync(account);

        _logger.LogInformation("Deleted account with Id {Id}", account.Id);
    }

    public async Task EditAccountAsync(Account account)
    {
        await _accountService.EditAccountAsync(account);

        _logger.LogInformation("Edited account with Id {Id}", account.Id);
    }

    public async Task<Account> GetAccountByIdAsync(int id)
    {
        var account = await _accountService.GetAccountByIdAsync(id);

        _logger.LogInformation("Account fetched: {account}", JsonSerializer.Serialize(account));

        return account;
    }

    public async Task<List<Account>> GetAllAccountsAsync()
    {
        var accounts = await _accountService.GetAllAccountsAsync();

        _logger.LogInformation("Accounts fetched: {accounts}", accounts.Count);
        accounts.ForEach(account => _logger.LogInformation("Account fetched: {account}", JsonSerializer.Serialize(account)));

        return accounts;
    }

    public async Task TransferMoneyAsync(IFormCollection form, decimal value)
    {
        await _accountService.TransferMoneyAsync(form, value);

        _logger.LogInformation("Transfered {value} zł from Account with Id {accountFrom} to Account with Id {accountTo}", value, form["AccountFrom"], form["AccountTo"]);
    }
}
