using Application.Helpers;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class SavingService : ISavingService
    {
        private readonly ISavingRepository _serviceRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly UserHelper _userHelper;

        public SavingService(ISavingRepository savingRepository, UserHelper userHelper, IAccountRepository accountRepository)
        {
            _serviceRepository = savingRepository;
            _accountRepository = accountRepository;
            _userHelper = userHelper;
        }

        public async Task<Saving> GetSavingByIdAsync(int id) => await _serviceRepository.GetSavingByIdAsync(id);

        public async Task<Saving> GetSavingByBudgetIdAsync(int id) => await _serviceRepository.GetSavingByBudgetIdAsync(id);

        public async Task<List<Saving>> GetAllSavingsAsync() => await _serviceRepository.GetAllSavingsAsync(_userHelper.GetUserId()!);

        public async Task CreateSavingAsync(Saving saving)
        {
            saving.UserId = _userHelper.GetUserId()!;

            await _serviceRepository.CreateSavingAsync(saving);
        }

        public async Task EditSavingAsync(int id, decimal value, decimal aimValue)
        {
            var saving = await GetSavingByIdAsync(id);
            var account = await _accountRepository.GetAccountByIdAsync(saving.BudgetId);

            account.Blockade = value;
            saving.Value = value;
            saving.AimValue = aimValue;

            await _accountRepository.EditAccountAsync(account);
            await _serviceRepository.EditSavingAsync(saving);
        }

        public async Task DeleteSavingAsync(int id)
        {
            var saving = await GetSavingByIdAsync(id);
            var account = await _accountRepository.GetAccountByIdAsync(saving.BudgetId);

            account.Blockade -= saving.Value;

            await _accountRepository.EditAccountAsync(account);
            await _serviceRepository.DeleteSavingAsync(saving);
        }

        public async Task SavingSupplyMoney(IFormCollection form, decimal value)
        {
            var saving = await GetSavingByIdAsync(Convert.ToInt32(form["ID"]));
            var account = await _accountRepository.GetAccountByIdAsync(saving.BudgetId);

            account.Blockade += value;
            saving.Value += value;

            await _accountRepository.EditAccountAsync(account);
            await _serviceRepository.EditSavingAsync(saving);
        }
    }
}
