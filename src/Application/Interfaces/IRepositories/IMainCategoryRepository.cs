using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface IMainCategoryRepository
{
	Task<MainCategory> GetMainCategoryByIdAsync(int id);
	Task<List<MainCategory>> GetAllMainCategoriesAsync(string userId);
	Task CreateMainCategoryAsync(MainCategory mainCategory);
	Task EditMainCategoryAsync(MainCategory mainCategory);
	Task DeleteMainCategoryAsync(MainCategory mainCategory, List<Category> categories, List<Operation> operations);
}