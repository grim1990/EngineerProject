using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext : DbContext
{
	public virtual DbSet<Account> Accounts { get; set; }
	public virtual DbSet<MainCategory> MainCategories { get; set; }
	public virtual DbSet<Category> Categories { get; set; }
	public virtual DbSet<Operation> Operations { get; set; }
	public virtual DbSet<Saving> Savings { get; set; }

	public DataContext(DbContextOptions<DataContext> options) : base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{

		modelBuilder.Entity<Account>()
			.Property(a => a.Blockade)
			.HasPrecision(18, 2);

		modelBuilder.Entity<Account>()
			.Property(a => a.Value)
			.HasPrecision(18, 2);

		modelBuilder.Entity<Category>()
			.Property(a => a.Value)
			.HasPrecision(18, 2);

		modelBuilder.Entity<Operation>()
			.Property(a => a.Value)
			.HasPrecision(18, 2);

		modelBuilder.Entity<Saving>()
			.Property(a => a.Value)
			.HasPrecision(18, 2);

		modelBuilder.Entity<Saving>()
			.Property(a => a.AimValue)
			.HasPrecision(18, 2);
	}
}