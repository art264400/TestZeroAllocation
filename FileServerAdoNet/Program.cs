using Microsoft.Extensions.Configuration;
using Npgsql;

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

string connectionString = config["ConnectionStrings"];

try
{
    using var connection = new NpgsqlConnection(connectionString);
    connection.Open();
    if (connection.State == System.Data.ConnectionState.Open)
    {
        using var command = new NpgsqlCommand("select \"Link\" from \"FK\".\"Document\" where \"Link\" <> '' order by \"Link\" ", connection);
        using NpgsqlDataReader reader = command.ExecuteReader();
        using var fileWriter = new StreamWriter("links.txt");

        while (reader.Read())
        {
            string link = reader.GetString(0);
            fileWriter.WriteLine(link); 
        }
    }
    Console.WriteLine("Data has been written to the file.");
}
catch (NpgsqlException ex)
{
    Console.WriteLine($"Ошибка при работе с PostgreSQL: {ex.Message}");
}
catch(Exception ex)
{
    Console.WriteLine($"Неожиданная ошибка: {ex.Message}");
}

