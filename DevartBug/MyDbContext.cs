using Microsoft.EntityFrameworkCore;

namespace BugReplication
{
	public class MyDbContext : DbContext
	{
		protected readonly string ConnectionString;

		public MyDbContext(string connectionString)
		{
			ConnectionString = connectionString;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseMySql(ConnectionString);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Record>();
		}
	}
}
