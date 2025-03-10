using Application.Commands;
using Application.Helpers;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Domain.Entities;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IOperationService _operationService;
        private readonly UserHelper _userHelper;

        public CategoryService(ICategoryRepository categoryRepository, IOperationService operationService, UserHelper userHelper)
        {
            _categoryRepository = categoryRepository;
            _operationService = operationService;
            _userHelper = userHelper;
        }

        public async Task<Category> GetCategoryByIdAsync(int id) => await _categoryRepository.GetCategoryByIdAsync(id);

        public async Task<List<Category>> GetAllCategoriesAsync() => await _categoryRepository.GetAllCategoriesAsync(_userHelper.GetUserId()!);

        public async Task<List<Category>> GetAllCategoriesByTypeAsync(int categoryType)
        {
            var categories = await GetAllCategoriesAsync();
            var filteredCategories = categories.Where(c => c.Type == categoryType && c.CreateDate.Year == DateTime.Now.Year || c.Type == categoryType && c.IsAnnual == true);

            return filteredCategories.ToList();
        }

        public async Task<List<Category>> GetAllFilteredCategoriesByOperations(int categoryId, int month, int year, int type)
        {
            var categoriesByType = await GetAllCategoriesByTypeAsync(type);
            var operations = await _operationService.GetFilteredOperationsByCategoryIdMonthYearAsync(categoryId, month, year);
            var filteredOperations = await _operationService.GetOperationDataType(operations, type);
            var categories = CategoryCommands.CalculateCategoryOperationsValue(categoriesByType, filteredOperations);

            return categories;
        }

        public async Task CreateCategoryAsync(Category category)
        {
            category.UserId = _userHelper.GetUserId()!;
            category.CreateDate = DateTime.Now;

            await _categoryRepository.CreateCategoryAsync(category);
        }

        public async Task EditCategoryAsync(Category category) => await _categoryRepository.EditCategoryAsync(category);

        public async Task DeleteCategoryAsync(Category category) => await _categoryRepository.DeleteCategoryAsync(category);
    }
}
