using Application.Interfaces.IServices;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Api.Controllers;

[Authorize]
public class SavingsController : Controller
{
    private readonly IAccountService _accountService;
    private readonly ISavingService _savingService;

    public SavingsController(IAccountService accountService, ISavingService savingService)
    {
        _savingService = savingService;
        _accountService = accountService;
    }

    public async Task<ActionResult> Index()
    {
        var savings = await _savingService.GetAllSavingsAsync();

        return View(savings);
    }

    public async Task<ActionResult> Details(int id)
    {
        var saving = await _savingService.GetSavingByIdAsync(id);

        return View(saving);
    }

    public async Task<ActionResult> CreateAsync()
    {
        var accounts = await _accountService.GetAllAccountsAsync();
        ViewBag.Accounts = new SelectList(accounts, "Id", "Name");

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(Saving saving)
    {
        await _savingService.CreateSavingAsync(saving);

        return RedirectToAction(nameof(Index));
    }

    public async Task<ActionResult> Edit(int id)
    {
        var saving = await _savingService.GetSavingByIdAsync(id);

        return View(saving);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, decimal value, decimal aimValue)
    {
        await _savingService.EditSavingAsync(id, value, aimValue);
        return RedirectToAction(nameof(Index));
    }

    public async Task<ActionResult> Delete(int id)
    {
        var saving = await _savingService.GetSavingByIdAsync(id);

        return View(saving);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Delete(Saving saving, int id)
    {
        await _savingService.DeleteSavingAsync(id);

        return RedirectToAction(nameof(Index));
    }

    public async Task<ActionResult> Supply(int id)
    {
        var saving = await _savingService.GetSavingByIdAsync(id);

        return View(saving);
    }

    [HttpPost]
    public async Task<IActionResult> SavingSupplyMoney(IFormCollection form, decimal value)
    {
        await _savingService.SavingSupplyMoney(form, value);
        return Redirect("/Savings");
    }
}
