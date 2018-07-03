using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BugReplication
{
	internal static class Program
	{
		private const int Times = 12;

		static void Main(string[] args)
		{
			Console.WriteLine("Start");

			try
			{
				var tasks = Enumerable.Range(1, Times)
					.Select(i => Task.Run(async () =>
					{
						var dateTime = DateTime.Parse("2018-06-25");
						var db = NewDbContext();
						var data = await db.Set<Record>()
							.Where(r => !r.Bool && r.Date.Date == dateTime.Date)
							.ToListAsync();
					}));

				Task.WaitAll(tasks.ToArray());
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			Console.WriteLine("Finish!");
			Console.ReadKey();
		}

		private static MyDbContext NewDbContext()
		   => new MyDbContext($"Server=172.30.57.193; Port=3306; Database=test; Uid=root; Pwd=root; CharSet=utf8; License Key={LicenseKey};");

		private const string LicenseKey =
			"";
	}
}
