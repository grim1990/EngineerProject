using Domain.Entities;
using FluentAssertions;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Infrastructure.Integration.Tests.Repositories;

public class AccountRepositoryTests : IClassFixture<DatabaseFixture>
{
	private readonly AccountRepository _accountRepository;
	private readonly DataContext _context;

	public AccountRepositoryTests(DatabaseFixture fixture)
	{
		_context = fixture.Context;
		_accountRepository = new AccountRepository(_context);
	}

	[Fact]
	public void GetAccountByIdAsync_ShouldReturn_Account()
	{
		var account = new Account
		{
			Id = 1,
			Name = "TestAccount",
			UserId = "TestUser"
		};

		_context.Accounts.Add(account);
		_context.SaveChanges();

		var actual = _accountRepository.GetAccountByIdAsync(1).Result;

		actual.Should().NotBeNull();
		actual.Should().BeEquivalentTo(account, options => options.ExcludingMissingMembers());
	}

	[Fact]
	public void GetAllAccountsAsync_ShouldReturn_AllAccounts()
	{
		var accounts = new List<Account>
		{
			new Account
			{
				Id = 2,
				Name = "TestAccount1",
				UserId = "TestUser1"
			},
			new Account
			{
				Id = 3,
				Name = "TestAccount2",
				UserId = "TestUser1"
			},
		};

		_context.Accounts.AddRange(accounts);
		_context.SaveChanges();

		var actual = _accountRepository.GetAllAccountsAsync("TestUser1").Result;

		actual.Should().NotBeNull();
		actual.Should().BeEquivalentTo(accounts, options => options.ExcludingMissingMembers());
	}
}