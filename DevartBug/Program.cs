using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BugReplication
{
	internal static class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Start");
			var db = NewDbContext();

			for (var i = 0; i < 10; i++)
			{
				Task.Run(async () =>
				{
					await db.Set<Record>()
						.AsNoTracking()
						.Where(_ => Enumerable.Range(1, 2).ToArray().Contains(_.Id))
						.Skip(20)
						.Take(25)
						.OrderBy(x => x.String)
						.ToListAsync();
				}).Wait();

				Console.WriteLine();
			}

			Console.WriteLine("Finish");
			Console.ReadLine();
		}

		static MyDbContext NewDbContext()
			=> new MyDbContext($"Server=172.30.57.193; Port=3306; Database=test; Uid=root; Pwd=root; CharSet=utf8; License Key={LicenseKey};");

		const string LicenseKey =
			"";
	}
}
