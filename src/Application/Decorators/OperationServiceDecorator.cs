using Application.Interfaces.IServices;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using X.PagedList;

namespace Application.Decorators;

public class OperationServiceDecorator : IOperationService
{
    private readonly IOperationService _operationService;
    private readonly ILogger _logger;

    public OperationServiceDecorator(IOperationService operationService, ILogger logger)
    {
        _operationService = operationService;
        _logger = logger;
    }

    public async Task CreateOperationAsync(Domain.Entities.Operation operation)
    {
        await _operationService.CreateOperationAsync(operation);

        _logger.LogInformation("Created operation {operation}", JsonSerializer.Serialize(operation));
    }

    public async Task DeleteOperationAsync(Domain.Entities.Operation operation)
    {
        await _operationService.DeleteOperationAsync(operation);

        _logger.LogInformation("Deleted operation with Id {operationId}", operation.Id);
    }

    public async Task EditOperationAsync(Domain.Entities.Operation actualOperation, Domain.Entities.Operation operation)
    {
        await _operationService.EditOperationAsync(actualOperation, operation);

        _logger.LogInformation("Edited operation with Id {operationId}", actualOperation.Id);
    }

    public async Task<List<Domain.Entities.Operation>> GetAllOperationsAsync()
    {
        var operations = await _operationService.GetAllOperationsAsync();

        _logger.LogInformation("Operations fetched: {operations}", operations.Count);
        operations.ForEach(operation => _logger.LogInformation("Operation fetched: {operation}", JsonSerializer.Serialize(operation)));

        return operations;
    }

    public async Task<List<Domain.Entities.Operation>> GetFilteredOperationsByCategoryIdMonthYearAsync(int categoryId, int month, int year)
    {
        var operations = await _operationService.GetFilteredOperationsByCategoryIdMonthYearAsync(categoryId, month, year);

        _logger.LogInformation("Filtered Operations fetched: {operations}", operations.Count);
        operations.ForEach(operation => _logger.LogInformation("Filtered Operation fetched: {operation}", JsonSerializer.Serialize(operation)));

        return operations;
    }

    public async Task<List<int>> GetFilteredOperationsYearsAsync()
    {
        var operations = await _operationService.GetFilteredOperationsYearsAsync();

        _logger.LogInformation("Filtered Operations years fetched: {operations}", operations.Count);
        operations.ForEach(operation => _logger.LogInformation("Filtered Operation year fetched: {operation}", JsonSerializer.Serialize(operation)));

        return operations;
    }

    public async Task<List<Domain.Entities.Operation>> GetLastTenOperations()
    {
        var operations = await _operationService.GetLastTenOperations();

        _logger.LogInformation("Last ten Operations fetched: {operations}", operations.Count);
        operations.ForEach(operation => _logger.LogInformation("Last ten Operation fetched: {operation}", JsonSerializer.Serialize(operation)));

        return operations;
    }

    public async Task<Domain.Entities.Operation> GetOperationByIdAsync(int id)
    {
        var operation = await _operationService.GetOperationByIdAsync(id);

        _logger.LogInformation("Operation fetched: {operation}", JsonSerializer.Serialize(operation));

        return operation;
    }

    public async Task<List<Domain.Entities.Operation>> GetOperationDataType(List<Domain.Entities.Operation> operations, int operationType)
    {
        var operationsType = await _operationService.GetOperationDataType(operations, operationType);

        _logger.LogInformation("Operations type fetched: {operations}", operations.Count);
        operationsType.ForEach(operation => _logger.LogInformation("Operation type fetched: {operation}", JsonSerializer.Serialize(operation)));

        return operationsType;
    }

    public async Task<decimal> GetOperationDataTypeSum(List<Domain.Entities.Operation> operations, int operationType)
    {
        var operationsTypeSum = await _operationService.GetOperationDataTypeSum(operations, operationType);

        _logger.LogInformation("Operations type sum fetched: {operationTypeSum}", operationsTypeSum);

        return operationsTypeSum;
    }

    public IPagedList<Domain.Entities.Operation> PagedOperations(List<Domain.Entities.Operation> operations, int? page, int? pageSize, out int totalItemCount)
    {
        var pagedOperations = _operationService.PagedOperations(operations, page, pageSize, out totalItemCount);

        _logger.LogInformation("Paged Operations fetched: {pagedOperations}", pagedOperations);

        return pagedOperations;
    }
}
