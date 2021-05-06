using System;
using System.IO;

namespace GraphMaxClickSearcher
{
    internal class Program
    {
        const string GraphInputFileName = "graph.txt";

        static void Main()
        {
            IGraphReader graphReader = new GraphFileReader(GraphInputFileName);

            long[] graph = null;
            try
            {
                graph = graphReader.ReadGraph();
            }
            catch (FormatException)
            {
                Console.WriteLine("Граф задан в неверном формате.");
            }
            catch (IOException)
            {
                Console.WriteLine($"Ошибка при чтении графа с файла {GraphInputFileName}.");
            }
            catch
            {
                Console.WriteLine("Что-то пошло не так... Попробуйте перезагрузить программу.");
            }

            var clickSearcher = new GraphClickSearcher();
            var maxClick = clickSearcher.GetMaxClick(graph);

            Console.WriteLine("Максимальная клика состоит из вершин: ");
            Console.WriteLine(string.Join(" ", maxClick));
        }
    }
}
