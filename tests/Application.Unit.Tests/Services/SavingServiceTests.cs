using Application.Helpers;
using Application.Interfaces.IRepositories;
using Application.Services;
using Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using NSubstitute;

namespace Application.Unit.Tests.Services;

public class SavingServiceTests
{
	private readonly SavingService _sut;
	private readonly UserHelper _userHelperMock;
	private readonly ISavingRepository _savingRepositoryMock = Substitute.For<ISavingRepository>();
	private readonly IAccountRepository _accountRepisitoryMock = Substitute.For<IAccountRepository>();
	private readonly IHttpContextAccessor _httpContextAccessor = Substitute.For<IHttpContextAccessor>();

	public SavingServiceTests()
	{
		_userHelperMock = new UserHelper(_httpContextAccessor);
		_sut = new SavingService(_savingRepositoryMock, _userHelperMock, _accountRepisitoryMock);
	}

	[Fact]
	public async Task GetAllSavingsAsync_ShouldReturn_AllSavings()
	{
		var expectedSavings = new List<Saving>
		{
			new Saving
			{
				Id = 1,
				UserId = "1",
				Name = "SavingTestName1"
			},
			new Saving
			{
				Id = 2,
				UserId = "1",
				Name = "SavingTestName2"
			},
		};

		_sut.GetAllSavingsAsync().Returns(expectedSavings);

		var result = await _sut.GetAllSavingsAsync();

		result.Should().BeEquivalentTo(expectedSavings);

		await _savingRepositoryMock.Received(1).GetAllSavingsAsync(Arg.Any<string>());
	}

	[Fact]
	public async Task GetSavingByIdAsync_ShouldReturn_Saving()
	{
		var savingId = 1;
		var expectedSaving = new Saving
		{
			Id = savingId,
		};

		_sut.GetSavingByIdAsync(savingId).Returns(expectedSaving);

		var result = await _sut.GetSavingByIdAsync(savingId);

		result.Should().BeEquivalentTo(expectedSaving);

		await _savingRepositoryMock.Received(1).GetSavingByIdAsync(Arg.Is(savingId));
	}

	[Fact]
	public async Task CreateSavingAsync_ShouldCreateSaving_WithCorrectParameters()
	{
		var expectedSaving = new Saving
		{
			Id = 1,
			UserId = "1",
			Name = "CreateTestName1",
			Description = "CreateTestDescription1"
		};

		await _sut.CreateSavingAsync(expectedSaving);

		await _savingRepositoryMock.Received(1).CreateSavingAsync(Arg.Is<Saving>(saving =>
			saving.Id == expectedSaving.Id &&
			saving.Name == expectedSaving.Name &&
			saving.UserId == expectedSaving.UserId &&
			saving.Description == expectedSaving.Description
		));
	}

	[Fact]
	public async Task EditSavingAsync_ShouldEdit_Saving()
	{
		var expectedSaving = new Saving
		{
			Id = 1,
			UserId = "1",
			Name = "EditTestName1",
			Description = "EditTestDescription1",
			Value = 100,
			AimValue = 100
		};

		var account = new Account
		{
			Id = 1,
			Value = 100
		};

		_savingRepositoryMock.GetSavingByIdAsync(expectedSaving.Id).Returns(expectedSaving);
		_accountRepisitoryMock.GetAccountByIdAsync(expectedSaving.BudgetId).Returns(account);

		await _sut.EditSavingAsync(expectedSaving.Id, expectedSaving.Value, expectedSaving.AimValue);

		await _savingRepositoryMock.Received(1).EditSavingAsync(Arg.Is<Saving>(saving =>
			saving.Id == expectedSaving.Id &&
			saving.Name == expectedSaving.Name &&
			saving.UserId == expectedSaving.UserId &&
			saving.Description == expectedSaving.Description &&
			saving.Value == expectedSaving.Value &&
			saving.AimValue == expectedSaving.AimValue
		));
	}

	[Fact]
	public async Task DeleteSavingAsync_ShouldDelete_Saving()
	{
		var saving = new Saving
		{
			Id = 1,
			BudgetId = 1,
			Value = 100,
			AimValue = 100
		};

		var account = new Account
		{
			Id = 1,
			Value = 100
		};

		_savingRepositoryMock.GetSavingByIdAsync(saving.Id).Returns(saving);
		_accountRepisitoryMock.GetAccountByIdAsync(saving.BudgetId).Returns(account);

		await _sut.DeleteSavingAsync(saving.Id);

		await _savingRepositoryMock.Received(1).DeleteSavingAsync(saving);
	}
}
