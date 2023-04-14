using LibraryTest;
using System.Threading.Tasks;

namespace _9_3._TestTargetNetCore3._1
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var fileOperation = new FileOperations();
            _ = await fileOperation.ReadFileLinesAsync("‪new_sql_vrs.txt");
        }
    }
}
