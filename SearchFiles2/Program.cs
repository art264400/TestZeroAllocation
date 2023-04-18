using Microsoft.Extensions.Configuration;
using System.Diagnostics;

class Program
{
    private static long _totalFiles;
    private static long _totalSize;
    private static Timer _timer;

    static void Main()
    {
        var sw = new Stopwatch();
        sw.Start();
        // Загрузка конфигурации
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();


        string folderToScan = config["FolderToScan"];

        // Создание и запуск таймера
        _timer = new Timer(DisplayProgress, null, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(10));

        // Выполнение сканирования папки
        Parallel.ForEach(Directory.GetDirectories(folderToScan), subfolder =>
        {
            ScanFolder(subfolder);
        });

        // Остановка таймера
        _timer.Dispose();

        sw.Stop();
        Console.WriteLine("Завершено.");
        Console.WriteLine($"Общее количество файлов: {_totalFiles}");
        Console.WriteLine($"Общий размер файлов: {_totalSize / 1024.0 / 1024.0} МБ");
        Console.WriteLine($"Заняло времени {sw.Elapsed}");
        Console.ReadLine();
    }

    private static void ScanFolder(string folder)
    {
        foreach (var file in Directory.GetFiles(folder))
        {
            var fileInfo = new FileInfo(file);
            Interlocked.Increment(ref _totalFiles);
            Interlocked.Add(ref _totalSize, fileInfo.Length);
        }

        foreach (var subfolder in Directory.GetDirectories(folder))
        {
            ScanFolder(subfolder);
        }
    }

    private static void DisplayProgress(object state)
    {
        Console.WriteLine($"На данный момент: {_totalFiles} файлов, {_totalSize / (1024.0 * 1024.0):F2} МБ");
    }
}