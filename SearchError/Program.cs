using System.Text.RegularExpressions;

string logFilePath = "C:\\Users\\Arthur\\Documents\\logs24.04\\vrs-lk\\lanit_ucfk_portal_log_20230424.log";

try
{
    var errorCount = GetErrorCounts(logFilePath);
    Console.WriteLine("Наиболее часто встречающиеся ошибки:");

    foreach (var error in errorCount)
    {
        Console.WriteLine($"Ошибка: {error.Key}, Количество: {error.Value}");
    }
}
catch (Exception ex)
{
    Console.WriteLine("Произошла ошибка при чтении лог-файла: " + ex.Message);
}

static Dictionary<string, int> GetErrorCounts(string logFilePath)
{
    Dictionary<string, int> errorCount = new Dictionary<string, int>();
    string line;

    string errorPattern = @"{""Timestamp"":""[^""]+"",""Level"":""Error""[^}]*}";
    string messagePattern = @"""MessageTemplate"":""([^""]+)""";

    using (StreamReader sr = new StreamReader(logFilePath))
    {
        while ((line = sr.ReadLine()) != null)
        {
            if (Regex.IsMatch(line, errorPattern))
            {
                var messageMatch = Regex.Match(line, messagePattern);
                if (messageMatch.Success)
                {
                    string errorMessage = messageMatch.Groups[1].Value;

                    if (errorCount.ContainsKey(errorMessage))
                    {
                        errorCount[errorMessage]++;
                    }
                    else
                    {
                        errorCount[errorMessage] = 1;
                    }
                }
            }
        }
    }

    return errorCount;
}
