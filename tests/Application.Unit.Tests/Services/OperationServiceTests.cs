using Application.Helpers;
using Application.Interfaces.IRepositories;
using Application.Services;
using Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;

namespace Application.Unit.Tests.Services;

public class OperationServiceTests
{
	private readonly OperationService _sut;
	private readonly UserHelper _userHelper;
	private readonly IOperationRepository _operationRepositoryMock = Substitute.For<IOperationRepository>();
	private readonly IAccountRepository _accountRepositoryMock = Substitute.For<IAccountRepository>();
	private readonly IHttpContextAccessor _httpContextAccessorMock = Substitute.For<IHttpContextAccessor>();

	public OperationServiceTests()
	{
		_userHelper = new UserHelper(_httpContextAccessorMock);
		_sut = new OperationService(_operationRepositoryMock, _accountRepositoryMock, _userHelper);
	}

	[Fact]
	public async Task GetAllOperationsAsync_ShouldReturn_AllOperations()
	{
		var expectedOperations = new List<Operation>
		{
			new Operation
			{
				Id = 1,
			},
			new Operation
			{
				Id = 2,
			}
		};

		_sut.GetAllOperationsAsync().Returns(expectedOperations);

		var result = await _sut.GetAllOperationsAsync();

		result.Should().BeEquivalentTo(expectedOperations);

		await _operationRepositoryMock.Received(1).GetAllOperationsAsync(Arg.Any<string>());
	}

	[Fact]
	public async Task GetOperationByIdAsync_ShouldReturn_Operation()
	{
		var operationId = 1;
		var expectedOperation = new Operation
		{
			Id = operationId,
		};

		_sut.GetOperationByIdAsync(operationId).Returns(expectedOperation);

		var result = await _sut.GetOperationByIdAsync(operationId);

		result.Should().BeEquivalentTo(expectedOperation);

		await _operationRepositoryMock.Received(1).GetOperationByIdAsync(Arg.Is(operationId));
	}

	[Fact]
	public async Task CreateOperationAsync_ShouldCreateOperation_WithCorrectParameters()
	{
		var expectedOperation = new Operation
		{
			Id = 1,
			UserId = "1",
			BudgetId = 1,
			Description = "CreateTestDescription1",
			ValuesString = "100"
		};

		var account = new Account
		{
			Id = 1,
			UserId = "1",
			Name = "Test",
			Description = "Test",
			Value = 100
		};

		_accountRepositoryMock.GetAccountByIdAsync(expectedOperation.BudgetId).Returns(account);

		await _sut.CreateOperationAsync(expectedOperation);
		await _operationRepositoryMock.Received(1).CreateOperationAsync(Arg.Is<Operation>(operation =>
			operation.Id == expectedOperation.Id &&
			operation.UserId == expectedOperation.UserId &&
			operation.Description == expectedOperation.Description));
	}

	[Fact]
	public async Task EditOperationAsync_ShouldEdit_Operation()
	{
		var expectedOperation = new Operation
		{
			Id = 1,
			UserId = "1",
			BudgetId = 1,
			Description = "EditTestDescription1",
			Value = 100
		};

		var actualOperation = new Operation
		{
			Id = 1,
			UserId = "1",
			BudgetId = 1,
			Description = "EditTestDescription1",
			Value = 0
		};

		var account = new Account
		{
			Id = 1,
			UserId = "1",
			Name = "Test",
			Description = "Test",
			Value = 100
		};

		_accountRepositoryMock.GetAccountByIdAsync(expectedOperation.BudgetId).Returns(account);

		await _sut.EditOperationAsync(actualOperation, expectedOperation);
		await _operationRepositoryMock.Received(1).EditOperationAsync(actualOperation, expectedOperation, Arg.Any<string>());
	}

	[Fact]
	public async Task DeleteOperationAsync_ShouldDelete_Operation()
	{
		var operation = new Operation
		{
			Id = 1,
			BudgetId = 1,
			Value = 100,
		};

		var account = new Account
		{
			Id = 1,
			Value = 100
		};

		_accountRepositoryMock.GetAccountByIdAsync(1).Returns(account);

		await _sut.DeleteOperationAsync(operation);
		await _operationRepositoryMock.Received(1).DeleteOperationAsync(operation);
	}
}
