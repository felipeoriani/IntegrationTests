using System;
using MongoDB.Driver;

namespace IntegrationTest.Tests
{
	public class MongoFixture
	{
		public IMongoDatabase Database { get; }

		public IMongoCollection<Product> Products => Database.GetCollection<Product>("products");
		
		public MongoFixture()
		{
			var connectionString = Environment.GetEnvironmentVariable("connectionString") ?? "mongodb://localhost:27017";
			
			var client = new MongoClient(connectionString);
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