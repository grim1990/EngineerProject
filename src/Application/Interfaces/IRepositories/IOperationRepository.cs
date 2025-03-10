using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface IOperationRepository
{
    Task<Operation> GetOperationByIdAsync(int id);
    Task<List<Operation>> GetAllOperationsAsync(string userId);
    Task CreateOperationAsync(Operation operation);
    Task EditOperationAsync(Operation actualOperation, Operation operation, string userId);
    Task DeleteOperationAsync(Operation operation);
}