using Application.Helpers;
using Application.Interfaces.IRepositories;
using Application.Services;
using Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;

namespace Application.Unit.Tests.Services;

public class AccountServiceTests
{
	private readonly AccountService _sut;
	private readonly UserHelper _userHelperMock;
	private readonly IAccountRepository _accountRepositoryMock = Substitute.For<IAccountRepository>();
	private readonly ISavingRepository _savingRepositoryMock = Substitute.For<ISavingRepository>();
	private readonly IHttpContextAccessor _httpContextAccessorMock = Substitute.For<IHttpContextAccessor>();

	public AccountServiceTests()
	{
		_userHelperMock = new UserHelper(_httpContextAccessorMock);
		_sut = new AccountService(_accountRepositoryMock, _savingRepositoryMock, _userHelperMock);
	}

	[Fact]
	public async Task GetAllAccountsAsync_ShouldReturn_AllAccounts()
	{
		var expectedAccounts = new List<Account>
		{
			new Account
			{
				Id = 1,
				UserId = "1",
				Name = "TestName1",
				Description = "TestDescription1"
			},
			new Account
			{
				Id = 2,
				UserId = "1",
				Name = "TestName2",
				Description = "TestDescription2"
			},
		};

		_sut.GetAllAccountsAsync().Returns(expectedAccounts);

		var result = await _sut.GetAllAccountsAsync();

		result.Should().BeEquivalentTo(expectedAccounts);

		await _accountRepositoryMock.Received(1).GetAllAccountsAsync(Arg.Any<string>());
	}

	[Fact]
	public async Task GetAccountByIdAsync_ShouldReturn_CorrectAccount()
	{
		var accountId = 1;
		var expectedAccount = new Account { Id = accountId, Name = "TestName1" };

		_sut.GetAccountByIdAsync(accountId).Returns(expectedAccount);

		var result = await _sut.GetAccountByIdAsync(accountId);

		result.Should().BeEquivalentTo(expectedAccount);

		await _accountRepositoryMock.Received(1).GetAccountByIdAsync(Arg.Is(accountId));
	}

	[Fact]
	public async Task CreateAccountAsync_ShouldCreate_AccountWithCorrectParameters()
	{
		var expectedAccount = new Account
		{
			Id = 1,
			UserId = "1",
			Name = "CreateTestName1",
			Description = "CreateTestDescription1"
		};

		await _sut.CreateAccountAsync(expectedAccount);

		await _accountRepositoryMock.Received(1).CreateAccountAsync(Arg.Is<Account>(account =>
			account.Id == expectedAccount.Id &&
			account.Name == expectedAccount.Name &&
			account.UserId == expectedAccount.UserId &&
			account.Description == expectedAccount.Description
		));
	}

	[Fact]
	public async Task EditAccountAsync_ShouldEdit_Account()
	{
		var expectedAccount = new Account
		{
			Id = 1,
			UserId = "1",
			Name = "EditTestName1",
			Description = "EditTestDescription1"
		};

		await _sut.EditAccountAsync(expectedAccount);

		await _accountRepositoryMock.Received(1).EditAccountAsync(Arg.Is<Account>(account =>
			account.Id == expectedAccount.Id &&
			account.Name == expectedAccount.Name &&
			account.UserId == expectedAccount.UserId &&
			account.Description == expectedAccount.Description
		));
	}

	[Fact]
	public async Task DeleteAccountAsync_ShouldDelete_AccountWithNoAssociatedSavings()
	{
		var account = new Account
		{
			Id = 1,
		};

		_savingRepositoryMock.GetSavingByBudgetIdAsync(account.Id).Returns((Saving)null!);

		await _sut.DeleteAccountAsync(account);

		await _accountRepositoryMock.Received(1).DeleteAccountAsync(account);
	}

	[Fact]
	public async Task DeleteAccountAsync_ShouldThrowException_WithAssociatedSavings()
	{
		var account = new Account
		{
			Id = 1,
		};

		var associatedSaving = new Saving
		{
			Id = 1,
		};

		_savingRepositoryMock.GetSavingByBudgetIdAsync(account.Id).Returns(associatedSaving);

		await Assert.ThrowsAsync<InvalidOperationException>(async () =>
		{
			await _sut.DeleteAccountAsync(account);
		});
	}
}
