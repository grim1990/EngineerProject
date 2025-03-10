using Domain.Entities;
using FluentAssertions;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Infrastructure.Integration.Tests.Repositories;

public class CategoryRepositoryTests : IClassFixture<DatabaseFixture>
{
	private readonly CategoryRepository _categoryRepository;
	private readonly DataContext _context;

	public CategoryRepositoryTests(DatabaseFixture fixture)
	{
		_context = fixture.Context;
		_categoryRepository = new CategoryRepository(_context);
	}

	[Fact]
	public void GetCategoryByIdAsync_ShouldReturn_Category()
	{
		var category = new Category
		{
			Id = 1,
			Name = "TestCategory",
			UserId = "TestUser"
		};
		_context.Categories.Add(category);
		_context.SaveChanges();

		var actual = _categoryRepository.GetCategoryByIdAsync(1).Result;

		actual.Should().NotBeNull();
		actual.Should().BeEquivalentTo(category, options => options.ExcludingMissingMembers());
	}

	[Fact]
	public void GetAllCategoriesAsync_ShouldReturn_AllCategories()
	{
		var categories = new List<Category>
		{
			new Category
			{
				Id = 2,
				Name = "TestCategory1",
				UserId = "TestUser1"
			},
			new Category
			{
				Id = 3,
				Name = "TestCategory2",
				UserId = "TestUser1"
			},
		};

		_context.Categories.AddRange(categories);
		_context.SaveChanges();

		var actual = _categoryRepository.GetAllCategoriesAsync("TestUser1").Result;

		actual.Should().NotBeNull();
		actual.Should().BeEquivalentTo(categories, options => options.ExcludingMissingMembers());
	}
}
