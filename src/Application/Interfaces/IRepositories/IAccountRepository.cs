using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface IAccountRepository
{
    Task<Account> GetAccountByIdAsync(int id);
    Task<List<Account>> GetAllAccountsAsync(string id);
    Task CreateAccountAsync(Account account);
    Task EditAccountAsync(Account account);
    Task DeleteAccountAsync(Account account);
}