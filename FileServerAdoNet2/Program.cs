//using Microsoft.Extensions.Configuration;
//using Npgsql;
//using System.Data;
//using System.Diagnostics;

//var config = new ConfigurationBuilder()
//    .SetBasePath(Directory.GetCurrentDirectory())
//    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//    .Build();
//var sw = new Stopwatch();
//sw.Start();
//string connectionString = config["ConnectionStrings"];
//try
//{
//    using var connection = new NpgsqlConnection(connectionString);

//    connection.Open();

//    int pageSize = int.Parse(config["Limit"]);
//    int offset = 0;
//    int rowsFetched;
//    using var fileWriter = new StreamWriter("links.txt");
//    do
//    {
//        rowsFetched = 0;

//        using var command = new NpgsqlCommand();
//        command.Connection = connection;
//        command.CommandType = CommandType.Text;
//        command.CommandText = @"select ""Link"" from ""FK"".""Document"" 
//                                                 where ""Link"" <> '' 
//                                                 order by ""Link"" 
//                                                 LIMIT @pageSize
//                                                 OFFSET @offset;";

//        command.Parameters.AddWithValue("@pageSize", pageSize);
//        command.Parameters.AddWithValue("@offset", offset);

//        using var reader = command.ExecuteReader();

//        using NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, connection);
//        while (reader.Read())
//        {
//            fileWriter.WriteLine(reader[0].ToString());
//            rowsFetched++;
//        }

//        offset += pageSize;
//    }
//    while (rowsFetched == pageSize);
//    Console.WriteLine("Data has been written to the file.");
//}
//catch (NpgsqlException ex)
//{
//    Console.WriteLine($"Ошибка при работе с PostgreSQL: {ex.Message}");
//}
//catch (Exception ex)
//{
//    Console.WriteLine($"Неожиданная ошибка: {ex.Message}");
//}
//sw.Stop();
//Console.WriteLine($"Прошло времени: {sw.Elapsed}");
//Console.ReadLine();