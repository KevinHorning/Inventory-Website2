using CTG.Database.MySQL;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Test1
{
    public static void Main()
    {
        test().Wait();
        Console.ReadKey();
    }

    public static async Task test()
	{
		MySqlDatabaseManager manager = new MySqlDatabaseManager("server=localhost;database=atlas;uid=kevin;password=kevin");
		manager.GetConnection();

        Query insQuery = insertQuery();
        manager.ExecuteNonQueryAsync(manager.GetConnection(), insQuery.QueryString, insQuery.Parameters).Wait();

		using (var reader = await manager.ExecuteReaderAsync(manager.GetConnection(), "SELECT * from users"))
		{
			while (reader.Read())
			{
				Console.WriteLine(reader[0] + " " + reader[1] + " " + reader[2]);
			}
		}
		Console.WriteLine("Done.");
	}

    public static Query insertQuery()
    {
        var username = new KeyValuePair<String, Object>("userName", "John Smith");
        var password = new KeyValuePair<String, Object>("hashString", "84345354");
        var values = new[] { username, password };
        Query query = QueryBuilder.BuildInsertQuery("users", values);
        return query;
    }
}