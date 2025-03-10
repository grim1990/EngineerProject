using Application.Helpers;
using Application.Interfaces.IRepositories;
using Application.Services;
using Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;

namespace Application.Unit.Tests.Services;

public class MainCategoryServiceTests
{
	private readonly MainCategoryService _sut;
	private readonly UserHelper _userHelper;
	private readonly IMainCategoryRepository _mainCategoryRepositoryMock = Substitute.For<IMainCategoryRepository>();
	private readonly ICategoryRepository _categoryRepositoryMock = Substitute.For<ICategoryRepository>();
	private readonly IOperationRepository _operationRepositoryMock = Substitute.For<IOperationRepository>();
	private readonly IHttpContextAccessor _httpContextAccessorMock = Substitute.For<IHttpContextAccessor>();

	public MainCategoryServiceTests()
	{
		_userHelper = new UserHelper(_httpContextAccessorMock);
		_sut = new MainCategoryService(_mainCategoryRepositoryMock, _categoryRepositoryMock, _operationRepositoryMock, _userHelper);
	}

	[Fact]
	public async Task GetAllMainCategoriesAsync_ShouldReturn_AllMainCategories()
	{
		var expectedMainCategories = new List<MainCategory>
		{
			new MainCategory
			{
				Id = 1,
				UserId = "1",
				Name = "MainCategoryTestName1"
			},
			new MainCategory
			{
				Id = 2,
				UserId = "1",
				Name = "MainCategoryTestName2"
			},
		};

		_sut.GetAllMainCategoriesAsync().Returns(expectedMainCategories);

		var result = await _sut.GetAllMainCategoriesAsync();

		result.Should().BeEquivalentTo(expectedMainCategories);

		await _mainCategoryRepositoryMock.Received(1).GetAllMainCategoriesAsync(Arg.Any<string>());
	}

	[Fact]
	public async Task GetMainCategoryByIdAsync_ShouldReturn_CorrectMainCategory()
	{
		var mainCategoryId = 1;
		var expectedMainCategory = new MainCategory { Id = mainCategoryId, Name = "TestName1" };

		_sut.GetMainCategoryByIdAsync(mainCategoryId).Returns(expectedMainCategory);

		var result = await _sut.GetMainCategoryByIdAsync(mainCategoryId);

		result.Should().BeEquivalentTo(expectedMainCategory);

		await _mainCategoryRepositoryMock.Received(1).GetMainCategoryByIdAsync(Arg.Is(mainCategoryId));
	}

	[Fact]
	public async Task CreateMainCategoryAsync_ShouldCreate_MainCategoryWithCorrectParameters()
	{
		var expectedMainCategory = new MainCategory
		{
			Id = 1,
			UserId = "1",
			Name = "CreateTestName1",
			Description = "CreateTestDescription1"
		};

		await _sut.CreateMainCategoryAsync(expectedMainCategory);

		await _mainCategoryRepositoryMock.Received(1).CreateMainCategoryAsync(Arg.Is<MainCategory>(mainCategory =>
			mainCategory.Id == expectedMainCategory.Id &&
			mainCategory.Name == expectedMainCategory.Name &&
			mainCategory.UserId == expectedMainCategory.UserId &&
			mainCategory.Description == expectedMainCategory.Description
		));
	}

	[Fact]
	public async Task EditMainCategoryAsync_ShouldEdit_MainCategory()
	{
		var expectedMainCategory = new MainCategory
		{
			Id = 1,
			UserId = "1",
			Name = "EditTestName1",
			Description = "EditTestDescription1"
		};

		await _sut.EditMainCategoryAsync(expectedMainCategory);

		await _mainCategoryRepositoryMock.Received(1).EditMainCategoryAsync(Arg.Is<MainCategory>(mainCategory =>
			mainCategory.Id == expectedMainCategory.Id &&
			mainCategory.Name == expectedMainCategory.Name &&
			mainCategory.UserId == expectedMainCategory.UserId &&
			mainCategory.Description == expectedMainCategory.Description
		));
	}

	[Fact]
	public async Task DeleteMainCategoryAsync_ShouldDelete_MainCategoryAndRelatedCategories()
	{
		var mainCategory = new MainCategory { Id = 1 };

		var categories = new List<Category>
		{
			new Category { Id = 1, MainCategoryId = 1 },
			new Category { Id = 1, MainCategoryId = 1 },
			new Category { Id = 2, MainCategoryId = 2 },
		};

		var operations = new List<Operation>
		{
			new Operation { CategoryId = 1 },
			new Operation { CategoryId = 1 },
			new Operation { CategoryId = 2 },
		};

		_operationRepositoryMock.GetAllOperationsAsync(Arg.Any<string>()).Returns(Task.FromResult(operations));
		_categoryRepositoryMock.GetAllCategoriesAsync(Arg.Any<string>()).Returns(Task.FromResult(categories));
		_mainCategoryRepositoryMock.GetMainCategoryByIdAsync(Arg.Any<int>()).Returns(Task.FromResult(mainCategory));

		await _sut.DeleteMainCategoryAsync(mainCategory);

		await _mainCategoryRepositoryMock
			.Received(1)
			.DeleteMainCategoryAsync(mainCategory, Arg.Is<List<Category>>(list => list.Count == 2), Arg.Is<List<Operation>>(list => list.Count == 2));
	}
}
