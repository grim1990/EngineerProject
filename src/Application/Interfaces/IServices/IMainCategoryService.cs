using Domain.Entities;

namespace Application.Interfaces.IServices;

public interface IMainCategoryService
{
    Task<MainCategory> GetMainCategoryByIdAsync(int id);
    Task<List<MainCategory>> GetAllMainCategoriesAsync();
    Task<List<int>> GetFilteredMainCategoriesYearsAsync();
    Task CreateMainCategoryAsync(MainCategory mainCategory);
    Task DeleteMainCategoryAsync(MainCategory mainCategory);
    Task EditMainCategoryAsync(MainCategory mainCategory);
    Task<List<MainCategory>> GetAllMainCategoriesFromCurrentYearAsync();
}