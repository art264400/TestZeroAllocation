string inputFilePath = "log.log"; // Путь к исходному файлу
string outputFilePath = "output.txt"; // Путь к выходному файлу

try
{
    using StreamReader sr = new StreamReader(inputFilePath);
    using StreamWriter sw = new StreamWriter(outputFilePath);

    string line;
    while ((line = sr.ReadLine()) != null)
    {
        if (line.Contains("ChangeCertificateStatus/CertificatesList") && line.Contains("500"))
        {
            sw.WriteLine(line);
        }
    }
}
catch (Exception e)
{
    Console.WriteLine($"Произошла ошибка: {e.Message}");
}