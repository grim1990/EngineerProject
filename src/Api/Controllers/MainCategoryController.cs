using Application.Interfaces.IServices;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize]
public class MainCategoryController : Controller
{
    private readonly IMainCategoryService _mainCategoryService;

    public MainCategoryController(IMainCategoryService mainCategoryService)
    {
        _mainCategoryService = mainCategoryService;
    }

    public async Task<IActionResult> Index()
    {
        var mainCategories = await _mainCategoryService.GetAllMainCategoriesAsync();

        return View(mainCategories);
    }

    public async Task<IActionResult> Details(int id)
    {
        var mainCategory = await _mainCategoryService.GetMainCategoryByIdAsync(id);

        return View(mainCategory);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MainCategory mainCategory)
    {
        await _mainCategoryService.CreateMainCategoryAsync(mainCategory);

        return Redirect("/Home/BudgetPlan");
    }

    public async Task<IActionResult> Edit(int id)
    {
        var mainCategory = await _mainCategoryService.GetMainCategoryByIdAsync(id);

        return View(mainCategory);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(MainCategory mainCategory)
    {
        await _mainCategoryService.EditMainCategoryAsync(mainCategory);

        return Redirect("/Home/BudgetPlan");
    }

    public async Task<IActionResult> Delete(int id)
    {
        var mainCategory = await _mainCategoryService.GetMainCategoryByIdAsync(id);

        return View(mainCategory);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(MainCategory mainCategory)
    {
        await _mainCategoryService.DeleteMainCategoryAsync(mainCategory);

        return Redirect("/Home/BudgetPlan");
    }
}
