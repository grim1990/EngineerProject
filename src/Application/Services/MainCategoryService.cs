using Application.Helpers;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Domain.Entities;

namespace Application.Services
{
	public class MainCategoryService : IMainCategoryService
	{
		private readonly IMainCategoryRepository _mainCategoryRepository;
		private readonly ICategoryRepository _categoryRepository;
		private readonly IOperationRepository _operationRepository;
		private readonly UserHelper _userHelper;

		public MainCategoryService(IMainCategoryRepository mainCategoryRepository,
			ICategoryRepository categoryRepository,
			IOperationRepository operationRepository,
			UserHelper userHelper)
		{
			_mainCategoryRepository = mainCategoryRepository;
			_categoryRepository = categoryRepository;
			_operationRepository = operationRepository;
			_userHelper = userHelper;
		}

		public async Task<MainCategory> GetMainCategoryByIdAsync(int id) => await _mainCategoryRepository.GetMainCategoryByIdAsync(id);

		public async Task<List<MainCategory>> GetAllMainCategoriesAsync() => await _mainCategoryRepository.GetAllMainCategoriesAsync(_userHelper.GetUserId()!);

		public async Task CreateMainCategoryAsync(MainCategory mainCategory)
		{
			mainCategory.UserId = _userHelper.GetUserId()!;
			mainCategory.CreateDate = DateTime.Now;

			await _mainCategoryRepository.CreateMainCategoryAsync(mainCategory);
		}

		public async Task<List<MainCategory>> GetAllMainCategoriesFromCurrentYearAsync()
		{
			var mainCategories = await GetAllMainCategoriesAsync();

			var filteredCategories = mainCategories.Where(m => m.CreateDate.Year == DateTime.Now.Year || m.IsAnnual == true);

			return filteredCategories.ToList();
		}

		public async Task EditMainCategoryAsync(MainCategory mainCategory) => await _mainCategoryRepository.EditMainCategoryAsync(mainCategory);

		public async Task DeleteMainCategoryAsync(MainCategory mainCategory)
		{
			var categories = await _categoryRepository.GetAllCategoriesAsync(_userHelper.GetUserId()!);
			var operations = await _operationRepository.GetAllOperationsAsync(_userHelper.GetUserId()!);
			var filteredCategories = categories.Where(c => c.MainCategoryId == mainCategory.Id).ToList();
			var filteredOperations = new List<Operation>();

			foreach (var category in filteredCategories)
			{
				filteredOperations = operations.Where(o => o.CategoryId == category.Id).ToList();
			}

			var mainCategories = await GetMainCategoryByIdAsync(mainCategory.Id);

			await _mainCategoryRepository.DeleteMainCategoryAsync(mainCategories, filteredCategories, filteredOperations);
		}

		public async Task<List<int>> GetFilteredMainCategoriesYearsAsync()
		{
			var mainCategories = await GetAllMainCategoriesAsync();

			var filteredMainCategories = mainCategories
				.Select(m => m.CreateDate.Year)
				.Distinct()
				.ToList();

			return filteredMainCategories;
		}
	}
}
