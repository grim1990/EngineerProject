using Domain.Entities;
using FluentAssertions;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Infrastructure.Integration.Tests.Repositories;

public class OperationRepositoryTests : IClassFixture<DatabaseFixture>
{
	private readonly OperationRepository _operationRepository;
	private readonly DataContext _context;

	public OperationRepositoryTests(DatabaseFixture fixture)
	{
		_context = fixture.Context;
		_operationRepository = new OperationRepository(_context);
	}

	[Fact]
	public void GetOperationByIdAsync_ShouldReturn_Operation()
	{
		var operation = new Operation
		{
			Id = 1,
			UserId = "TestUser"
		};

		_context.Operations.Add(operation);
		_context.SaveChanges();

		var actual = _operationRepository.GetOperationByIdAsync(1).Result;

		actual.Should().NotBeNull();
		actual.Should().BeEquivalentTo(operation, options => options.ExcludingMissingMembers());
	}

	[Fact]
	public void GetAllOperationsAsync_ShouldReturn_AllOperations()
	{
		var operations = new List<Operation>
		{
			new Operation
			{
				Id = 2,
				UserId = "TestUser1"
			},
			new Operation
			{
				Id = 3,
				UserId = "TestUser1"
			},
		};

		_context.Operations.AddRange(operations);
		_context.SaveChanges();

		var actual = _operationRepository.GetAllOperationsAsync("TestUser1").Result;

		actual.Should().NotBeNull();
		actual.Should().BeEquivalentTo(operations, options => options.ExcludingMissingMembers());
	}
}