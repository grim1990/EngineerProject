using Application.Interfaces.IServices;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Api.Controllers;

public class HomeController : Controller
{
    private readonly IMainCategoryService _mainCategoryService;
    private readonly IOperationService _operationService;
    private readonly IBudgetService _budgetService;
    private readonly ICategoryService _categoryService;

    public HomeController(IMainCategoryService mainCategoryRepository, IOperationService operationService,
        IBudgetService budgetService, ICategoryService categoryService)
    {
        _mainCategoryService = mainCategoryRepository;
        _operationService = operationService;
        _budgetService = budgetService;
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index(int year)
    {
        var operations = await _operationService.GetLastTenOperations();
        var categories = await _categoryService.GetAllCategoriesAsync();
        var budgetPlan = _budgetService.BudgetPlan(year);

        if (User.Identity!.IsAuthenticated)
        {
            ViewBag.BudgetPlan = budgetPlan;
            ViewBag.CategoriesArray = categories;
            return View(operations);
        }
        else
        {
            return View("HomePage");
        }
    }

    [Authorize]
    public async Task<IActionResult> BudgetPlan(int year)
    {
        var budgetPlan = _budgetService.BudgetPlan(year);
        var filteredYears = await _mainCategoryService.GetFilteredMainCategoriesYearsAsync();

        ViewBag.Years = new SelectList(filteredYears);

        return View(budgetPlan);
    }

    [Authorize]
    public async Task<IActionResult> BudgetStatus(int year, int month)
    {
        var budgetStatus = _budgetService.BudgetStatus(year, month);
        var filteredYears = await _operationService.GetFilteredOperationsYearsAsync();

        ViewBag.Years = new SelectList(filteredYears);

        return View(budgetStatus);
    }

    [Authorize]
    public IActionResult BudgetPlanData(int year)
    {
        var budgetPlan = _budgetService.BudgetPlan(year);

        return Json(budgetPlan);
    }

    public IActionResult Rules()
    {
        return View("Rules");
    }

    public IActionResult More()
    {
        return View("More");
    }

    public IActionResult HowTo()
    {
        return View("HowTo");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}