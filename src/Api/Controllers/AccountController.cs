using Application.Interfaces.IServices;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Api.Controllers;

[Authorize]
public class AccountController : Controller
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public async Task<IActionResult> Index()
    {
        var accounts = await _accountService.GetAllAccountsAsync();

        ViewBag.Accounts = new SelectList(accounts, "Id", "Name");
        ViewBag.AccountsValue = accounts.Select(p => new KeyValuePair<string, decimal>(p.Name, p.Value)).ToArray();
        ViewBag.sumAccountsMoney = accounts.Sum(a => a.Value);
        ViewBag.sumBlockade = accounts.Sum(a => a.Blockade);

        return View(accounts);
    }

    [HttpPost]
    public async Task<IActionResult> Transfer(IFormCollection form, decimal value)
    {
        await _accountService.TransferMoneyAsync(form, value);

        return Redirect("/Account");
    }

    public async Task<IActionResult> Details(int id)
    {
        var account = await _accountService.GetAccountByIdAsync(id);

        return View(account);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Account account)
    {
        await _accountService.CreateAccountAsync(account);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var account = await _accountService.GetAccountByIdAsync(id);

        return View(account);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Account account)
    {
        await _accountService.EditAccountAsync(account);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var account = await _accountService.GetAccountByIdAsync(id);

        return View(account);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Account account)
    {
        try
        {
            await _accountService.DeleteAccountAsync(account);
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            TempData["ErrorMessage"] = ex.Message;
            return RedirectToAction(nameof(Delete), new { id = account.Id });
        }
    }

    public async Task<IActionResult> AccountMoneyToSavings()
    {
        var accounts = await _accountService.GetAllAccountsAsync();

        var sumAccountsMoney = accounts.Sum(a => a.Value);
        var sumBlockade = accounts.Sum(a => a.Blockade);

        var moneyList = new List<decimal>
        {
            sumAccountsMoney,
            sumBlockade
        };

        return Json(moneyList);
    }
}