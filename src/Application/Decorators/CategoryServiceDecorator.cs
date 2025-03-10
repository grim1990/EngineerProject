using Application.Interfaces.IServices;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Application.Decorators;

public class CategoryServiceDecorator : ICategoryService
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger _logger;

    public CategoryServiceDecorator(ICategoryService categoryService, ILogger logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    public async Task CreateCategoryAsync(Category category)
    {
        await _categoryService.CreateCategoryAsync(category);

        _logger.LogInformation("Created category: {category}", JsonSerializer.Serialize(category));
    }

    public async Task DeleteCategoryAsync(Category category)
    {
        await _categoryService.DeleteCategoryAsync(category);

        _logger.LogInformation("Deleted category with Id: {categoryId}", category.Id);
    }

    public async Task EditCategoryAsync(Category category)
    {
        await _categoryService.EditCategoryAsync(category);

        _logger.LogInformation("Edited category with Id: {categoryId}", category.Id);
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();

        _logger.LogInformation("Categories fetched: {categories}", categories.Count);
        categories.ForEach(category => _logger.LogInformation("Category fetched: {category}", JsonSerializer.Serialize(category)));

        return categories;
    }

    public async Task<List<Category>> GetAllCategoriesByTypeAsync(int categoryType)
    {
        var categories = await _categoryService.GetAllCategoriesByTypeAsync(categoryType);

        _logger.LogInformation("Categories fetched: {categories}", categories.Count);
        categories.ForEach(category => _logger.LogInformation("Category fetched: {category}", JsonSerializer.Serialize(category)));

        return categories;
    }

    public async Task<List<Category>> GetAllFilteredCategoriesByOperations(int categoryId, int month, int year, int type)
    {
        var categories = await _categoryService.GetAllFilteredCategoriesByOperations(categoryId, month, year, type);

        _logger.LogInformation("Filtered categories fetched: {categories}", categories.Count);
        categories.ForEach(category => _logger.LogInformation("Filtered category fetched: {category}", JsonSerializer.Serialize(category)));

        return categories;
    }

    public async Task<Category> GetCategoryByIdAsync(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);

        _logger.LogInformation("Category fetched: {category}", JsonSerializer.Serialize(category));

        return category;
    }
}
