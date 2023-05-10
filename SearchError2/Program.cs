using System.Text.RegularExpressions;

string logFilePath = "C:\\Users\\Arthur\\Documents\\logs24.04\\vrs-lk\\lanit_ucfk_portal_log_20230424.log";

try
{
    var errorInfo = GetErrorInfo(logFilePath);
    var results = errorInfo.OrderByDescending(c => c.Value).ToList();
    Console.WriteLine("Наиболее часто встречающиеся ошибки:");

    foreach (var error in results)
    {
        Console.WriteLine($"Ошибка: {error.Key.Message}, Количество: {error.Value}");
        Console.WriteLine($"RequestPath: {error.Key.RequestPath}");
        if (!string.IsNullOrEmpty(error.Key.CommandText))
        {
            Console.WriteLine($"commandText: {error.Key.CommandText}");
        }
        Console.WriteLine();
    }
}
catch (Exception ex)
{
    Console.WriteLine("Произошла ошибка при чтении лог-файла: " + ex.Message);
}

static Dictionary<ErrorData, int> GetErrorInfo(string logFilePath)
{
    Dictionary<ErrorData, int> errorInfo = new Dictionary<ErrorData, int>();
    string line;

    string errorPattern = @"{""Timestamp"":""[^""]+"",""Level"":""Error""[^}]*}";
    string messagePattern = @"""MessageTemplate"":""([^""]+)""";
    string requestPathPattern = @"""RequestPath"":""([^""]+)""";
    string commandTextPattern = @"""commandText"":""([^""]+)""";

    using (StreamReader sr = new StreamReader(logFilePath))
    {
        while ((line = sr.ReadLine()) != null)
        {
            if (Regex.IsMatch(line, errorPattern))
            {
                var messageMatch = Regex.Match(line, messagePattern);
                var requestPathMatch = Regex.Match(line, requestPathPattern);
                var commandTextMatch = Regex.Match(line, commandTextPattern);

                if (messageMatch.Success && requestPathMatch.Success)
                {
                    string errorMessage = messageMatch.Groups[1].Value;
                    string requestPath = requestPathMatch.Groups[1].Value;
                    string commandText = commandTextMatch.Success ? commandTextMatch.Groups[1].Value : null;

                    var errorData = new ErrorData(errorMessage, requestPath, commandText);

                    if (errorInfo.ContainsKey(errorData))
                    {
                        errorInfo[errorData]++;
                    }
                    else
                    {
                        errorInfo[errorData] = 1;
                    }
                }
            }
        }
    }

    return errorInfo;
}

class ErrorData
{
    public string Message { get; }
    public string RequestPath { get; }
    public string CommandText { get; }

    public ErrorData(string message, string requestPath, string commandText)
    {
        Message = message;
        RequestPath = requestPath;
        CommandText = commandText;
    }

    public override bool Equals(object obj)
    {
        return obj is ErrorData data &&
               Message == data.Message &&
               RequestPath == data.RequestPath &&
               CommandText == data.CommandText;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Message, RequestPath, CommandText);
    }
}