using System;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace IntegrationTest.Tests
{
	public class Repo
	{
		private readonly string _connectionString;
		
		private IMongoDatabase Database { get; }

		public IMongoCollection<Product> Products => Database.GetCollection<Product>("products");
		
		public Repo(IOptions<DatabaseConfig> options)
		{
			_connectionString = options.Value.MyConnectionString;
			
			var client = new MongoClient(_connectionString);
			Database = client.GetDatabase("IntegrationTests");

			Products.InsertOne(new Product()
			{
				Id = Guid.NewGuid().ToString(),
				Name = "Guitar",
				Price = 1000m
			});

			Products.InsertOne(new Product()
			{
				Id = Guid.NewGuid().ToString(),
				Name = "Bass Guitar",
				Price = 1200m
			});
			
			Products.InsertOne(new Product()
			{
				Id = Guid.NewGuid().ToString(),
				Name = "Acoustic Guitar",
				Price = 400m
			});

			Products.InsertOne(new Product()
			{
				Id = Guid.NewGuid().ToString(),
				Name = "Acoustic Bass Guitar",
				Price = 800m
			});
		}
	}
}