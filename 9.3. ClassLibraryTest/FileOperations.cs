namespace _9._3._ClassLibraryTest
{
    public class FileOperations
    {
        public async Task<string[]> ReadFileLinesAsync(string filePath)
        {

#if NET6_0_OR_GREATER
            // Для .NET 6 используем асинхронный метод
            await Console.Out.WriteLineAsync("Для .NET 6 используем асинхронный метод");
            return await File.ReadAllLinesAsync(filePath);

#else
            await Console.Out.WriteLineAsync(" Для .NET Core 3.1 используем синхронный метод и оборачиваем его в Task");
            // Для .NET Core 3.1 используем синхронный метод и оборачиваем его в Task
            return await Task.Run(() => File.ReadAllLines(filePath));
#endif
        }
    }
}