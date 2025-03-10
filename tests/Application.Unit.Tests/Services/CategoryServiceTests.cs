using Application.Helpers;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Application.Services;
using Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;

namespace Application.Unit.Tests.Services;

public class CategoryServiceTests
{
	private readonly CategoryService _sut;
	private readonly UserHelper _userHelper;
	private readonly ICategoryRepository _categoryRepositoryMock = Substitute.For<ICategoryRepository>();
	private readonly IOperationService _operationServiceMock = Substitute.For<IOperationService>();
	private readonly IHttpContextAccessor _httpContextAccessorMock = Substitute.For<IHttpContextAccessor>();

	public CategoryServiceTests()
	{
		_userHelper = new UserHelper(_httpContextAccessorMock);
		_sut = new CategoryService(_categoryRepositoryMock, _operationServiceMock, _userHelper);
	}

	[Fact]
	public async Task GetAllCategoriesAsync_ShouldReturn_AllCategories()
	{
		var expectedCategories = new List<Category>
		{
			new Category
			{
				Id = 1,
				UserId = "1",
				Name = "CategoryTestName1"
			},
			new Category
			{
				Id = 2,
				UserId = "1",
				Name = "CategoryTestName2"
			},
		};

		_sut.GetAllCategoriesAsync().Returns(expectedCategories);

		var result = await _sut.GetAllCategoriesAsync();

		result.Should().BeEquivalentTo(expectedCategories);

		await _categoryRepositoryMock.Received(1).GetAllCategoriesAsync(Arg.Any<string>());
	}

	[Fact]
	public async Task GetCategoryByIdAsync_ShouldReturn_CorrectCategory()
	{
		var categoryId = 1;
		var expectedCategory = new Category { Id = categoryId, Name = "TestName1" };

		_sut.GetCategoryByIdAsync(categoryId).Returns(expectedCategory);

		var result = await _sut.GetCategoryByIdAsync(categoryId);

		result.Should().BeEquivalentTo(expectedCategory);

		await _categoryRepositoryMock.Received(1).GetCategoryByIdAsync(Arg.Is(categoryId));
	}

	[Fact]
	public async Task CreateCategoryAsync_ShouldCreateCategory_WithCorrectParameters()
	{
		var expectedCategory = new Category
		{
			Id = 1,
			UserId = "1",
			Name = "CreateTestName1",
			Description = "CreateTestDescription1"
		};

		await _sut.CreateCategoryAsync(expectedCategory);

		await _categoryRepositoryMock.Received(1).CreateCategoryAsync(Arg.Is<Category>(category =>
			category.Id == expectedCategory.Id &&
			category.Name == expectedCategory.Name &&
			category.UserId == expectedCategory.UserId &&
			category.Description == expectedCategory.Description
		));
	}

	[Fact]
	public async Task EditCategoryAsync_ShouldEdit_Category()
	{
		var expectedCategory = new Category
		{
			Id = 1,
			UserId = "1",
			Name = "EditTestName1",
			Description = "EditTestDescription1"
		};

		await _sut.EditCategoryAsync(expectedCategory);

		await _categoryRepositoryMock.Received(1).EditCategoryAsync(Arg.Is<Category>(category =>
			category.Id == expectedCategory.Id &&
			category.Name == expectedCategory.Name &&
			category.UserId == expectedCategory.UserId &&
			category.Description == expectedCategory.Description
		));
	}

	[Fact]
	public async Task DeleteCategoryAsync_ShouldDelete_Category()
	{
		var category = new Category
		{
			Id = 1,
		};

		await _sut.DeleteCategoryAsync(category);

		await _categoryRepositoryMock.Received(1).DeleteCategoryAsync(category);
	}
}
