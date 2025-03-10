using Domain.Entities;
using X.PagedList;

namespace Application.Interfaces.IServices;

public interface IOperationService
{
    Task<Operation> GetOperationByIdAsync(int id);
    Task<List<Operation>> GetAllOperationsAsync();
    Task<List<int>> GetFilteredOperationsYearsAsync();
    Task<List<Operation>> GetFilteredOperationsByCategoryIdMonthYearAsync(int categoryId, int month, int year);
    Task<List<Operation>> GetOperationDataType(List<Operation> operations, int operationType);
    Task CreateOperationAsync(Operation operation);
    Task EditOperationAsync(Operation actualOperation, Operation operation);
    Task DeleteOperationAsync(Operation operation);
    Task<decimal> GetOperationDataTypeSum(List<Operation> operations, int operationType);
    Task<List<Operation>> GetLastTenOperations();
    IPagedList<Operation> PagedOperations(List<Operation> operations, int? page, int? pageSize, out int totalItemCount);
}