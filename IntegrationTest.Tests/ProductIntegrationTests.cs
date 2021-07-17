using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using Xunit.Abstractions;
using Xunit;
using IntegrationTest.Tests.Util;

namespace IntegrationTest.Tests
{
	[TestCaseOrderer("IntegrationTest.Tests.Util.PriorityOrderer", "IntegrationTest.Tests")]
	public class ProductIntegrationTests : IClassFixture<MongoFixture>
	{
		private static Product _product;
		private readonly MongoFixture _mongoFixture;
		private readonly ITestOutputHelper _testOutput;

		public ProductIntegrationTests(MongoFixture mongoFixture,
			ITestOutputHelper testOutput)
		{
			_mongoFixture = mongoFixture;
			_testOutput = testOutput;
		}

		[Fact, TestPriority(1)]
		public async Task Products_Should_Have_Content()
		{
			var total = await _mongoFixture.Products.CountDocumentsAsync(new BsonDocument());
			_testOutput.WriteLine("Total in Products: " + total);

			Assert.True(total > 0);
		}

		[Fact, TestPriority(2)]
		public async Task Products_Should_Save_Product()
		{
			_product = new Product()
			{
				Id = Guid.NewGuid().ToString(),
				Name = "Drums",
				Price = 3000m
			};

			await _mongoFixture.Products.InsertOneAsync(_product);
			
			Assert.True(true);
		}
		
		[Fact, TestPriority(3)]
		public async Task Products_Should_Update_Product()
		{
			Expression<Func<Product, bool>> filter = x => x.Id == _product.Id;

			_product.Name = "Symbals";
			_product.Price = 1500m;

			var result = await _mongoFixture.Products.ReplaceOneAsync(filter, _product);

			Assert.True(result.ModifiedCount > 0);
		}
		
		[Fact, TestPriority(4)]
		public async Task Products_Should_Delete_Product()
		{
			Expression<Func<Product, bool>> filter = x => x.Id == _product.Id;

			var result = await _mongoFixture.Products.DeleteOneAsync(filter);

			Assert.True(result.DeletedCount > 0);
		}
	}

	/*
	 * Run a docker container
	 * docker run --name mongo -d -p 27017:27017 mongo
	 *
	 *
	 * 
	 */
}