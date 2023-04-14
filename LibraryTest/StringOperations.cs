using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryTest
{
    public class StringOperations
    {
        public string ConcatenateStrings(IEnumerable<string> strings)
        {
#if NETCOREAPP3_1
            Console.WriteLine("Для .NET Core 3.1 используем цикл для конкатенации строк");
            var sb = new StringBuilder();
            foreach (var str in strings)
            {
                sb.Append(str);
            }
            return sb.ToString();
#elif NET6_0_OR_GREATER
            Console.WriteLine("Для .NET 6 и выше используем метод string.Concat");
            return string.Concat(strings);
#else
            // В других случаях выбрасываем исключение или предоставляем альтернативную реализацию
            throw new NotSupportedException("This framework is not supported.");
#endif
        }
    }
}