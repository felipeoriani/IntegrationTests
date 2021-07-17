using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace IntegrationTest.Tests
{
	public class MongoFixture
	{
		public Repo Repo { get; }
		
		public MongoFixture()
		{
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json",  optional: true, reloadOnChange: false)
				.AddJsonFile($"appsettings.Development.json", optional: true, reloadOnChange: false)
				.AddEnvironmentVariables()
				.Build();
			
			var services = new ServiceCollection();

			services.Configure<DatabaseConfig>(options => configuration.GetSection(DatabaseConfig.ConfigurationKey).Bind(options));
			services.AddTransient<Repo>();

			var provider = services.BuildServiceProvider();

			this.Repo = provider.GetService<Repo>();
		}
	}
}