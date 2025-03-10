using Application.Interfaces.IServices;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Application.Decorators;

public class MainCategoryServiceDecorator : IMainCategoryService
{
    private readonly IMainCategoryService _mainCategoryService;
    private readonly ILogger _logger;

    public MainCategoryServiceDecorator(IMainCategoryService mainCategoryService, ILogger logger)
    {
        _mainCategoryService = mainCategoryService;
        _logger = logger;
    }

    public async Task CreateMainCategoryAsync(MainCategory mainCategory)
    {
        await _mainCategoryService.CreateMainCategoryAsync(mainCategory);

        _logger.LogInformation("Created main category: {mainCategory}", JsonSerializer.Serialize(mainCategory));
    }

    public async Task DeleteMainCategoryAsync(MainCategory mainCategory)
    {
        await _mainCategoryService.DeleteMainCategoryAsync(mainCategory);

        _logger.LogInformation("Deleted main category with id: {mainCategoryId}", mainCategory.Id);
    }

    public async Task EditMainCategoryAsync(MainCategory mainCategory)
    {
        await _mainCategoryService.EditMainCategoryAsync(mainCategory);

        _logger.LogInformation("Edited main category with id: {mainCategoryId}", mainCategory.Id);
    }

    public async Task<List<MainCategory>> GetAllMainCategoriesAsync()
    {
        var mainCategories = await _mainCategoryService.GetAllMainCategoriesAsync();

        _logger.LogInformation("Main categories fetched: {mainCategories}", mainCategories.Count);
        mainCategories.ForEach(mainCategory => _logger.LogInformation("Main category fetched: {mainCategory}", JsonSerializer.Serialize(mainCategory)));

        return mainCategories;
    }

    public async Task<List<MainCategory>> GetAllMainCategoriesFromCurrentYearAsync()
    {
        var mainCategories = await _mainCategoryService.GetAllMainCategoriesAsync();

        _logger.LogInformation("Main categories from current year fetched: {mainCategories}", mainCategories.Count);
        mainCategories.ForEach(mainCategory => _logger.LogInformation("Main category from current year fetched: {mainCategory}", JsonSerializer.Serialize(mainCategory)));

        return mainCategories;
    }

    public async Task<List<int>> GetFilteredMainCategoriesYearsAsync()
    {
        var filteredMainCategories = await _mainCategoryService.GetFilteredMainCategoriesYearsAsync();

        _logger.LogInformation("Filtered main categories fetched: {mainCategories}", filteredMainCategories.Count);
        filteredMainCategories.ForEach(filteredMainCategory => _logger.LogInformation("Main category from current year fetched: {mainCategory}", JsonSerializer.Serialize(filteredMainCategory)));

        return filteredMainCategories;
    }

    public async Task<MainCategory> GetMainCategoryByIdAsync(int id)
    {
        var mainCategory = await _mainCategoryService.GetMainCategoryByIdAsync(id);

        _logger.LogInformation("Main category fetched: {mainCategory}", JsonSerializer.Serialize(mainCategory));

        return mainCategory;
    }
}
