using Application.Interfaces.IServices;
using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Api.Controllers;

[Authorize]
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly IMainCategoryService _mainCategoryService;
    private readonly IOperationService _operationService;

    public CategoryController(ICategoryService categoryService,
        IMainCategoryService mainCategoryService, IOperationService operationService)
    {
        _categoryService = categoryService;
        _mainCategoryService = mainCategoryService;
        _operationService = operationService;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();

        return View(categories);
    }

    public async Task<IActionResult> Details(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        var mainCategory = await _mainCategoryService.GetMainCategoryByIdAsync(category.MainCategoryId);

        ViewBag.MainCategory = mainCategory.Name;

        return View(category);
    }

    public async Task<IActionResult> Create()
    {
        var mainCategories = await _mainCategoryService.GetAllMainCategoriesFromCurrentYearAsync();
        var categories = await _categoryService.GetAllCategoriesAsync();

        var categoriesList = categories.Select(c => new CustomSelectListItem
        {
            Type = c.Type,
            Name = c.Name,
            Description = c.Description,
            Value = c.Value,
            OneOrMonth = c.OneOrMonth,
            MainCategoryId = c.MainCategoryId
        }).ToList();

        ViewBag.MainCategories = new SelectList(mainCategories, "Id", "Name");
        ViewBag.Categories = categoriesList;

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Category category)
    {
        await _categoryService.CreateCategoryAsync(category);

        return Redirect("/Home/BudgetPlan");
    }

    public async Task<IActionResult> Edit(int id)
    {
        var mainCategories = await _mainCategoryService.GetAllMainCategoriesAsync();
        var category = await _categoryService.GetCategoryByIdAsync(id);

        ViewBag.MainCategories = new SelectList(mainCategories, "Id", "Name");

        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Category category)
    {
        await _categoryService.EditCategoryAsync(category);

        return Redirect("/Home/BudgetPlan");
    }

    public async Task<IActionResult> Delete(int id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        var mainCategory = await _mainCategoryService.GetMainCategoryByIdAsync(category.MainCategoryId);

        ViewBag.MainCategory = mainCategory.Name;

        return View(category);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Category category)
    {
        var operations = await _operationService.GetAllOperationsAsync();
        operations = operations.Where(o => o.CategoryId == category.Id).ToList();

        foreach (var operation in operations)
        {
            await _operationService.DeleteOperationAsync(operation);
        }

        await _categoryService.DeleteCategoryAsync(category);

        return Redirect("/Home/BudgetPlan");
    }

    [HttpGet]
    public async Task<IActionResult> GetFilteredCategoriesByOperations(int categoryId, int month, int year, int operationType)
    {
        var categories = await _categoryService.GetAllFilteredCategoriesByOperations(categoryId, month, year, operationType);

        return Json(categories);
    }
}