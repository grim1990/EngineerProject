using Domain.Entities;
using FluentAssertions;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Infrastructure.Integration.Tests.Repositories;

public class SavingRepositoryTests : IClassFixture<DatabaseFixture>
{
	private readonly SavingRepository _savingRepository;
	private readonly DataContext _context;

	public SavingRepositoryTests(DatabaseFixture fixture)
	{
		_context = fixture.Context;
		_savingRepository = new SavingRepository(_context);
	}

	[Fact]
	public void GetSavingByIdAsync_ShouldReturn_Saving()
	{
		var saving = new Saving
		{
			Id = 1,
			Name = "TestSaving",
			UserId = "TestUser"
		};

		_context.Savings.Add(saving);
		_context.SaveChanges();

		var actual = _savingRepository.GetSavingByIdAsync(1).Result;

		actual.Should().NotBeNull();
		actual.Should().BeEquivalentTo(saving, options => options.ExcludingMissingMembers());
	}

	[Fact]
	public void GetAllSavingsAsync_ShouldReturn_AllSavings()
	{
		var savings = new List<Saving>
		{
			new Saving
			{
				Id = 2,
				Name = "TestSaving1",
				UserId = "TestUser1"
			},
			new Saving
			{
				Id = 3,
				Name = "TestSaving2",
				UserId = "TestUser1"
			},
		};

		_context.Savings.AddRange(savings);
		_context.SaveChanges();

		var actual = _savingRepository.GetAllSavingsAsync("TestUser1").Result;

		actual.Should().NotBeNull();
		actual.Should().BeEquivalentTo(savings, options => options.ExcludingMissingMembers());
	}
}