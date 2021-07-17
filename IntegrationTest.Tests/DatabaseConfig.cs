namespace IntegrationTest.Tests
{
	public class DatabaseConfig
	{
		public static readonly string ConfigurationKey = "Database";
		
		public string MyConnectionString { get; set; }

		public DatabaseConfig()
		{
		}

		public DatabaseConfig(string myConnectionString)
		{
			MyConnectionString = myConnectionString;
		}
	}
}