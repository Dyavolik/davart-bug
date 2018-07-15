using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

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
			optionsBuilder.UseLoggerFactory(new LoggerFactory(new[]
				{new ConsoleLoggerProvider((log, logLevel) => logLevel == LogLevel.Information, true)}));
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Record>();
		}
	}
}
