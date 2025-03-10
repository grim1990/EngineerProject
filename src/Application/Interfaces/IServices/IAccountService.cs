using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.IServices;

public interface IAccountService
{
    Task<Account> GetAccountByIdAsync(int id);
    Task<List<Account>> GetAllAccountsAsync();
    Task CreateAccountAsync(Account account);
    Task DeleteAccountAsync(Account account);
    Task EditAccountAsync(Account account);
    Task TransferMoneyAsync(IFormCollection form, decimal value);
}