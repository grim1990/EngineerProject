using Application.Helpers;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Services;

public class AccountService : IAccountService
{
	private readonly IAccountRepository _accountRepository;
	private readonly ISavingRepository _savingRepository;
	private readonly UserHelper _userHelper;

	public AccountService(IAccountRepository accountRepository, ISavingRepository savingRepository, UserHelper userHelper)
	{
		_accountRepository = accountRepository;
		_savingRepository = savingRepository;
		_userHelper = userHelper;
	}

	public async Task<Account> GetAccountByIdAsync(int id) => await _accountRepository.GetAccountByIdAsync(id);

	public async Task<List<Account>> GetAllAccountsAsync() => await _accountRepository.GetAllAccountsAsync(_userHelper.GetUserId()!);

	public async Task CreateAccountAsync(Account account)
	{
		account.UserId = _userHelper.GetUserId()!;

		await _accountRepository.CreateAccountAsync(account);
	}

	public async Task EditAccountAsync(Account account) => await _accountRepository.EditAccountAsync(account);

	public async Task DeleteAccountAsync(Account account)
	{
		var saving = await _savingRepository.GetSavingByBudgetIdAsync(account.Id);

		if (saving != null)
		{
			throw new InvalidOperationException("Brak możliwości usunięcia konta - zweryfikuj powiązane z nim oszczędności!");
		}

		await _accountRepository.DeleteAccountAsync(account);
	}

	public async Task TransferMoneyAsync(IFormCollection form, decimal value)
	{
		var accountFrom = await GetAccountByIdAsync(Convert.ToInt32(form["AccountFrom"]));
		var accountTo = await GetAccountByIdAsync(Convert.ToInt32(form["AccountTo"]));

		accountFrom.Value -= value;
		accountTo.Value += value;

		await EditAccountAsync(accountFrom);
		await EditAccountAsync(accountTo);
	}
}
