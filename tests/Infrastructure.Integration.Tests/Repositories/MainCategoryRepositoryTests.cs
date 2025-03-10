using Domain.Entities;
using FluentAssertions;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Infrastructure.Integration.Tests.Repositories;

public class MainCategoryRepositoryTests : IClassFixture<DatabaseFixture>
{
	private readonly MainCategoryRepository _mainCategoryRepository;
	private readonly DataContext _context;

	public MainCategoryRepositoryTests(DatabaseFixture fixture)
	{
		_context = fixture.Context;
		_mainCategoryRepository = new MainCategoryRepository(_context);
	}

	[Fact]
	public void GetMainCategoryByIdAsync_ShouldReturn_MainCategory()
	{
		var mainCategory = new MainCategory
		{
			Id = 1,
			Name = "TestMainCategory",
			UserId = "TestUser"
		};

		_context.MainCategories.Add(mainCategory);
		_context.SaveChanges();

		var result = _mainCategoryRepository.GetMainCategoryByIdAsync(1).Result;

		result.Should().NotBeNull();
		result.Should().BeEquivalentTo(mainCategory, options => options.ExcludingMissingMembers());
	}

	[Fact]
	public void GetAllMainCategoriesAsync_ShouldReturn_AllMainCategories()
	{
		var mainCategories = new List<MainCategory>
		{
			new MainCategory
			{
				Id = 2,
				Name = "TestMainCategory1",
				UserId = "TestUser1"
			},
			new MainCategory
			{
				Id = 3,
				Name = "TestMainCategory2",
				UserId = "TestUser1"
			},
		};

		_context.MainCategories.AddRange(mainCategories);
		_context.SaveChanges();

		var result = _mainCategoryRepository.GetAllMainCategoriesAsync("TestUser1").Result;

		result.Should().NotBeNull();
		result.Should().BeEquivalentTo(mainCategories, options => options.ExcludingMissingMembers());
	}
}