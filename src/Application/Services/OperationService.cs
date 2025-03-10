using Application.Commands;
using Application.Helpers;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Domain.Entities;
using X.PagedList;

namespace Application.Services;

public class OperationService : IOperationService
{
    private readonly IOperationRepository _operationRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly UserHelper _userHelper;

    public OperationService(IOperationRepository operationRepository, IAccountRepository accountRepository, UserHelper userHelper)
    {
        _operationRepository = operationRepository;
        _accountRepository = accountRepository;
        _userHelper = userHelper;
    }

    public async Task<Operation> GetOperationByIdAsync(int id) => await _operationRepository.GetOperationByIdAsync(id);

    public async Task<List<Operation>> GetAllOperationsAsync() => await _operationRepository.GetAllOperationsAsync(_userHelper.GetUserId());

    public async Task<List<Operation>> GetFilteredOperationsByCategoryIdMonthYearAsync(int categoryId, int month, int year)
    {
        var operations = await GetAllOperationsAsync();

        if (categoryId != 0)
        {
            operations = operations.Where(o => o.CategoryId == categoryId).ToList();
        }
        if (month != 0)
        {
            operations = operations.Where(o => o.Month == month).ToList();
        }
        if (year != 0)
        {
            operations = operations.Where(o => o.CreateDate.Year == year).ToList();
        }

        return operations;
    }

    public Task<List<Operation>> GetOperationDataType(List<Operation> operations, int operationType)
    {
        var operationTypes = operations.Where(o => o.Type == operationType);

        return Task.FromResult(operationTypes.ToList());
    }

    public IPagedList<Operation> PagedOperations(List<Operation> operations, int? page, int? pageSize, out int totalItemCount)
    {
        int pageNumber = page ?? 1;
        int pageSizeValue = pageSize ?? 10;
        totalItemCount = operations.Count;

        var pagedOperations = operations
            .OrderByDescending(o => o.CreateDate)
            .AsQueryable()
            .ToPagedList(pageNumber, pageSizeValue, totalItemCount);

        return pagedOperations;
    }

    public async Task<List<int>> GetFilteredOperationsYearsAsync()
    {
        var operations = await GetAllOperationsAsync();

        var filteredOperations = operations
            .Select(o => o.CreateDate.Year)
            .Distinct()
            .ToList();

        return filteredOperations;
    }

    public async Task<List<Operation>> GetLastTenOperations()
    {
        var operations = await GetAllOperationsAsync();

        var filteredOperations = operations.TakeLast(10);

        return filteredOperations.ToList();
    }

    public Task<decimal> GetOperationDataTypeSum(List<Operation> operations, int operationType)
    {
        var operationData = GetOperationDataType(operations, operationType).Result;
        var operationSum = operationData.Sum(o => o.Value);

        return Task.FromResult(operationSum);
    }

    public async Task CreateOperationAsync(Operation operation)
    {
        var account = await _accountRepository.GetAccountByIdAsync(operation.BudgetId);

        operation.ValuesString ??= string.Empty;

        var values = operation.ValuesString.Replace(",", ".").Split(";");

        foreach (var value in values)
        {
            operation.Id = 0;
            operation.Value = decimal.Parse(value);
            operation.UserId = _userHelper.GetUserId()!;
            operation.CreateDate = DateTime.Now;
            OperationCommands.CreateExpenseOrIncomeOperation(operation, account!);

            await _accountRepository.EditAccountAsync(account!);
            await _operationRepository.CreateOperationAsync(operation);
        }
    }

    public async Task EditOperationAsync(Operation actualOperation, Operation operation)
    {
        var account = await _accountRepository.GetAccountByIdAsync(operation.BudgetId);

        OperationCommands.EditExpenseOrIncomeOperation(actualOperation, operation, account!);

        await _accountRepository.EditAccountAsync(account!);
        await _operationRepository.EditOperationAsync(actualOperation, operation, _userHelper.GetUserId()!);
    }

    public async Task DeleteOperationAsync(Operation operation)
    {
        var account = await _accountRepository.GetAccountByIdAsync(operation.BudgetId);

        OperationCommands.DeleteExpenseOrIncomeOperation(operation, account!);

        await _accountRepository.EditAccountAsync(account!);
        await _operationRepository.DeleteOperationAsync(operation);
    }
}