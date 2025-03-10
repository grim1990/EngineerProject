using Application.Interfaces.IServices;
using Application.Services;
using Domain.Entities;
using Domain.Models;
using FluentAssertions;
using NSubstitute;

namespace Application.Unit.Tests.Services;

public class BudgetServiceTests
{
	private readonly BudgetService _sut;
	private readonly IOperationService _operationServiceMock = Substitute.For<IOperationService>();
	private readonly IMainCategoryService _mainCategoryServiceMock = Substitute.For<IMainCategoryService>();
	private readonly ICategoryService _categoryServiceMock = Substitute.For<ICategoryService>();

	public BudgetServiceTests()
	{
		_sut = new BudgetService(_operationServiceMock, _mainCategoryServiceMock, _categoryServiceMock);
	}

	[Fact]
	public void BudgetPlan_ShouldReturn_BudgetPlanWithCorrectValues()
	{
		var expectedMainCategories = new List<MainCategory>()
		{
			new()
			{
				Id = 1,
				Name = "MainCategoryName1",
				Description = "MainCategoryDescription1",
				IsAnnual = true,
			},
			new()
			{
				Id = 2,
				Name = "MainCategoryName2",
				Description = "MainCategoryDescription2",
				IsAnnual = true,
			}
		};

		var expectedCategories = new List<Category>()
		{
			new()
			{
				Id = 1,
				Type = 1,
				Value= 100,
				Name = "CategoryName1",
				Description = "CategoryDescription1",
			},
			new()
			{
				Id = 2,
				Type = 0,
				Value= 200,
				Name = "CategoryName2",
				Description = "CategoryDescription2",
			}
		};

		var expectedBudgetPlan = new BudgetModel()
		{
			Categories = expectedCategories,
			MainCategories = expectedMainCategories,
			PlannedIncomes = 100,
			PlannedExpenses = 200,
		};

		_mainCategoryServiceMock.GetAllMainCategoriesAsync().Returns(expectedMainCategories);
		_categoryServiceMock.GetAllCategoriesAsync().Returns(expectedCategories);

		var result = _sut.BudgetPlan(0);

		result.Should().BeEquivalentTo(expectedBudgetPlan);
	}

	[Fact]
	public void BudgetStatus_ShouldReturn_BudgetStatusWithCorrectValues()
	{
		var expectedMainCategories = new List<MainCategory>()
		{
			new()
			{
				Id = 1,
				Name = "MainCategoryName1",
				Description = "MainCategoryDescription1",
				IsAnnual = true,
			},
			new()
			{
				Id = 2,
				Name = "MainCategoryName2",
				Description = "MainCategoryDescription2",
				IsAnnual = true,
			}
		};

		var expectedCategories = new List<Category>()
		{
			new()
			{
				Id = 1,
				Type = 1,
				Value= 100,
				Name = "CategoryName1",
				Description = "CategoryDescription1",
			},
			new()
			{
				Id = 2,
				Type = 0,
				Value= 200,
				Name = "CategoryName2",
				Description = "CategoryDescription2",
			}
		};

		var expectedOperations = new List<Operation>()
		{
			new()
			{
				Id = 1,
				Type = 1,
				Value= 100,
				Description = "OperationDescription1",
			},
			new()
			{
				Id = 2,
				Type = 0,
				Value= 200,
				Description = "OperationDescription2",
			}
		};

		var expectedBudgetStatus = new BudgetModel()
		{
			Categories = expectedCategories,
			MainCategories = expectedMainCategories,
			Operations = expectedOperations,
			Incomes = 100,
			Expenses = 200,
			PlannedIncomes = 100,
			PlannedExpenses = 200,
		};

		_mainCategoryServiceMock.GetAllMainCategoriesAsync().Returns(expectedMainCategories);
		_categoryServiceMock.GetAllCategoriesAsync().Returns(expectedCategories);
		_operationServiceMock.GetAllOperationsAsync().Returns(expectedOperations);

		var result = _sut.BudgetStatus(0, 0);

		result.Should().BeEquivalentTo(expectedBudgetStatus);
	}
}
