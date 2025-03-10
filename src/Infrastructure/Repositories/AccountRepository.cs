using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly DataContext _context;

    public AccountRepository(DataContext dataContext)
    {
        _context = dataContext;
    }

    public async Task<Account> GetAccountByIdAsync(int id)
    {
        var account = await _context.Accounts
                             .AsNoTracking()
                             .FirstOrDefaultAsync(a => a.Id == id);

        return account;
    }

    public async Task<List<Account>> GetAllAccountsAsync(string userId)
    {
        var accounts = await _context.Accounts
                              .Where(a => a.UserId == userId)
                              .AsNoTracking()
                              .ToListAsync();

        return accounts;
    }

    public async Task CreateAccountAsync(Account account)
    {
        await _context.Accounts.AddAsync(account);
        await _context.SaveChangesAsync();
    }

    public async Task EditAccountAsync(Account account)
    {
        await _context.Accounts
               .Where(a => a.Id == account.Id)
               .ExecuteUpdateAsync(p => p
               .SetProperty(x => x.Name, account.Name)
               .SetProperty(x => x.Description, account.Description)
               .SetProperty(x => x.Value, account.Value)
               .SetProperty(x => x.Blockade, account.Blockade));
    }

    public async Task DeleteAccountAsync(Account account)
    {
        await _context.Accounts
               .Where(a => a.Id == account.Id)
               .ExecuteDeleteAsync();
    }
}