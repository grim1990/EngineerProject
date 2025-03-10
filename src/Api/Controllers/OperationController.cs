using Application.Interfaces.IServices;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Api.Controllers;

[Authorize]
public class OperationController : Controller
{
    private readonly IOperationService _operationService;
    private readonly ICategoryService _categoryService;
    private readonly IAccountService _accountService;

    public OperationController(IOperationService operationService, ICategoryService categoryService,
        IAccountService accountService)
    {
        _operationService = operationService;
        _categoryService = categoryService;
        _accountService = accountService;
    }

    public async Task<IActionResult> Index(int categoryId, int month, int year, int? page, int? pageSize)
    {
        var operations = await _operationService.GetFilteredOperationsByCategoryIdMonthYearAsync(categoryId, month, year);
        var filteredYears = await _operationService.GetFilteredOperationsYearsAsync();
        var categories = await _categoryService.GetAllCategoriesAsync();
        var expenses = await _operationService.GetOperationDataTypeSum(operations, 0);
        var incomes = await _operationService.GetOperationDataTypeSum(operations, 1);
        var pagedOperations = _operationService.PagedOperations(operations, page, pageSize, out int totalItemCount);

        ViewData["categoryId"] = categoryId;
        ViewData["month"] = month;
        ViewData["year"] = year;

        ViewBag.TotalItemCount = totalItemCount;
        ViewBag.Years = new SelectList(filteredYears);
        ViewBag.Categories = new SelectList(categories, "Id", "Name");
        ViewBag.CategoriesArray = categories;
        ViewBag.Spends = expenses;
        ViewBag.Incomes = incomes;

        return View(pagedOperations);
    }

    public async Task<IActionResult> Details(int id)
    {
        var operation = await _operationService.GetOperationByIdAsync(id);
        var category = await _categoryService.GetCategoryByIdAsync(operation.CategoryId);
        var account = await _accountService.GetAccountByIdAsync(operation.BudgetId);

        ViewBag.Category = category.Name;
        ViewBag.Account = account.Name;

        return View(operation);
    }

    public async Task<IActionResult> CreateSpend()
    {
        var accounts = await _accountService.GetAllAccountsAsync();
        var categories = await _categoryService.GetAllCategoriesByTypeAsync(0);

        ViewBag.Accounts = new SelectList(accounts, "Id", "Name");
        ViewBag.Categories = new SelectList(categories, "Id", "Name");

        return View();
    }

    public async Task<IActionResult> CreateIncome()
    {
        var accounts = await _accountService.GetAllAccountsAsync();
        var categories = await _categoryService.GetAllCategoriesByTypeAsync(1);

        ViewBag.Accounts = new SelectList(accounts, "Id", "Name");
        ViewBag.Categories = new SelectList(categories, "Id", "Name");

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Operation operation)
    {
        await _operationService.CreateOperationAsync(operation);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var operation = await _operationService.GetOperationByIdAsync(id);
        var categories = await _categoryService.GetAllCategoriesAsync();
        var accounts = await _accountService.GetAllAccountsAsync();

        ViewBag.Categories = new SelectList(categories, "Id", "Name");
        ViewBag.Accounts = new SelectList(accounts, "Id", "Name");

        return View(operation);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Operation actualOperation)
    {
        var operation = await _operationService.GetOperationByIdAsync(id);

        await _operationService.EditOperationAsync(actualOperation, operation);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var operation = await _operationService.GetOperationByIdAsync(id);
        var category = await _categoryService.GetCategoryByIdAsync(operation.CategoryId);
        var account = await _accountService.GetAccountByIdAsync(operation.BudgetId);

        ViewBag.Category = category.Name;
        ViewBag.Account = account.Name;

        return View(operation);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, Operation operation)
    {
        operation = await _operationService.GetOperationByIdAsync(operation.Id);

        await _operationService.DeleteOperationAsync(operation);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> GetOperationData(int category, int month, int year, int operationType)
    {
        var operations = await _operationService.GetFilteredOperationsByCategoryIdMonthYearAsync(category, month, year);
        var operationData = await _operationService.GetOperationDataType(operations, operationType);

        return Json(operationData);
    }
}
