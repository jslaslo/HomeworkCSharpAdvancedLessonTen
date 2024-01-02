namespace Homework
{
    /*Объедините две предыдущих работы (практические работы 2 и 3): поиск файла и поиск текста в файле написав 
     * утилиту которая ищет файлы определенного расширения с указанным текстом. Рекурсивно. Пример вызова утилиты: 
     * utility.exe txt текст.*/
    internal class Program
    {
        const string searchText = "однако";
        const string list = "D:\\GeekBrines\\С#Advance\\Seminar\\LessonTen";
        static void Main(string[] args)
        {
            /*Console.WriteLine(list);
            var text = ReadFromText(searchText);
            var filter = Filter(list, text);
            Console.WriteLine(String.Join("\n", filter));*/
            List<string> files = SearchIn(path: args[0], name: args[1], filter: args[2]);

            Console.WriteLine(String.Join("\n", files));
        }
        private static List<string> SearchIn(string path, string name, string filter)
        {
            List<string> listFiles = new List<string>();

            DirectoryInfo dir = new DirectoryInfo(path);
            var directories = dir.GetDirectories();
            var files = dir.GetFiles();

            foreach (var file in files)
            {
                if (file.Name.Contains(name) && file.Extension.Contains(name))
                {
                    var text = ReadFromText(file.FullName);
                    if(Filter(filter, text))
                    {
                        listFiles.Add(file.FullName);
                    }                    
                }
            }
            foreach (var d in directories)
            {
                listFiles.AddRange(SearchIn(d.FullName, name, filter));
            }
            return listFiles;
        }
        static List<string> ReadFromText(string path)
        {
            List<string> result = new List<string>();
            using (StreamReader sr = new StreamReader(path))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    result.Add(line);
                    
                }
                return result;
            }
        }
        static bool Filter(string word, List<string> text)
        {
            return text.Where(x => x.Contains(word)).Any();
        }
    }
}
