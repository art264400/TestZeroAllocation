using Microsoft.Extensions.Configuration;
using System.Diagnostics;

class Program
{
    static int fileCount = 0;
    static long totalSize = 0;

    static void Main(string[] args)
    {
        var sw = new Stopwatch();
        sw.Start();
        var config = new ConfigurationBuilder()
           .SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .Build();

        string folderToScan = config["FolderToScan"] ?? string.Empty;
        Timer timer = new(DisplayProgress, null, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(10));

        CalculateFilesAndSize(folderToScan);

        timer.Change(Timeout.Infinite, Timeout.Infinite);
        timer.Dispose();
        sw.Stop();
        Console.WriteLine("Завершено.");
        Console.WriteLine($"Общее количество файлов: {fileCount}");
        Console.WriteLine($"Общий размер файлов: {totalSize / 1024.0 / 1024.0} МБ");
        Console.WriteLine($"Заняло времени {sw.Elapsed}");
        Console.ReadLine();
    }

    static void CalculateFilesAndSize(string folderPath)
    {
        foreach (var file in Directory.GetFiles(folderPath))
        {
            FileInfo fileInfo = new FileInfo(file);
            fileCount++;
            totalSize += fileInfo.Length;
        }

        foreach (var directory in Directory.GetDirectories(folderPath))
        {
            CalculateFilesAndSize(directory);
        }
    }

    static void DisplayProgress(object state)
    {
        Console.WriteLine($"На данный момент: {fileCount} файлов, {totalSize / 1024.0 / 1024.0} МБ");
    }
}