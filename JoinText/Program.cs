using System;
using System.Diagnostics;
using System.IO;

class Program
{
    static void Main()
    {
        string inputFilePath1 = "kapi.txt";
        string inputFilePath2 = "fzs.txt";
        string inputFilePath3 = "vrs.txt";
        string outputFilePath = "output.txt";

        try
        {
            using (var outputStream = File.CreateText(outputFilePath))
            {
                AppendFileContents(inputFilePath1, outputStream);
                AppendFileContents(inputFilePath2, outputStream);
                AppendFileContents(inputFilePath3, outputStream);
            }

            Console.WriteLine("Содержимое файлов успешно склеено в файл " + outputFilePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Произошла ошибка при склеивании файлов: " + ex.Message);
        }
    }

    static void AppendFileContents(string inputFilePath, StreamWriter outputStream)
    {
        using (var inputStream = File.OpenText(inputFilePath))
        {
            string line;
            while ((line = inputStream.ReadLine()) != null)
            {
                outputStream.WriteLine(line);
            }
        }
    }
}