using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Integration.Tests;

public class DatabaseFixture : IDisposable
{
	public DataContext Context { get; }

	public DatabaseFixture()
	{
		var options = new DbContextOptionsBuilder<DataContext>()
			.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
			.Options;
		Context = new DataContext(options);
		Context.Database.EnsureCreated();
	}

	public void Dispose()
	{
		Context.Database.EnsureDeleted();
		Context.Dispose();
	}
}