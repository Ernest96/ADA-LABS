using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Common
{
    public static class FileUtility
    {
        public static long[] ReadNumbersFromFile(string fileName)
        {
            string fileData = File.ReadAllText(fileName);
            var numbers = fileData.Split('\n').Select(x => Int64.Parse(x)).ToArray();
            Console.WriteLine($"In file are {numbers.Length} numbers.");


            return numbers;
        }

        public static void WriteNumbersToFile(string fileName, long[] numbers)
        {
            if (numbers != null)
            {
                string text = string.Join('\n', numbers);
                File.WriteAllText(fileName, text);

                Console.WriteLine($"Write succesfully ({numbers.Length} numbers)");
            }
        }

        public static void AppendNumberToFile(string fileName, long number)
        {
            using (StreamWriter sw = File.AppendText(fileName))
            {
                sw.WriteLine(number);
            }
        }

        public static void RecreateFile(string file)
        {
            FileInfo fi = new FileInfo(file);

            if (fi.Exists)
            {
                fi.Delete();
            }

            using (FileStream fs = fi.Create())
            {
            }
        }
    }
}
