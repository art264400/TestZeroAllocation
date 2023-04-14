using System.Buffers;
using System.IO.Pipelines;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var pipe = new Pipe();

        // Запускаем задачи для записи и чтения данных
        var writeTask = WriteDataAsync(pipe.Writer);
        var readTask = ReadDataAsync(pipe.Reader);
        Task.WaitAll(writeTask, readTask);
        Console.ReadLine();
    }

    private static async Task WriteDataAsync(PipeWriter writer)
    {
        // Записываем данные в PipeWriter 5 раз
        for (int i = 0; i < 5; i++)
        {
            // Получаем доступ к текущему свободному участку памяти
            Memory<byte> memory = writer.GetMemory(10);

            // Записываем строку в текущий участок памяти
            Encoding.ASCII.GetBytes($"Hello {i + 1}", memory.Span);

            // Указываем, что мы записали 10 байт данных
            writer.Advance(10);

            // Выполняем флаш буфера, означает что данные будут записаны в reader
            await writer.FlushAsync();

            // Ожидаем 1 секунду перед следующей записью
            await Task.Delay(1);
        }

        // Завершаем запись данных
        writer.Complete();
    }

    private static async Task ReadDataAsync(PipeReader reader)
    {
        while (true)
        {
            // Читаем данные из PipeReader
            ReadResult result = await reader.ReadAsync();
            ReadOnlySequence<byte> buffer = result.Buffer;

            // Обрабатываем каждый сегмент ReadOnlySequence
            foreach (ReadOnlyMemory<byte> segment in buffer)
            {
                // Выводим данные из каждого сегмента
                Console.WriteLine(Encoding.ASCII.GetString(segment.Span));
            }
            await Console.Out.WriteLineAsync();

            // Если все данные обработаны и получен сигнал об окончании записи, завершаем чтение
            if (result.IsCompleted)
            {
                break;
            }

            // Указываем, что обработали все данные
            reader.AdvanceTo(buffer.End);
        }

        // Завершаем чтение данных
        reader.Complete();
    }
}
