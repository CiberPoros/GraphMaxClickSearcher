using System;
using System.IO;

namespace GraphMaxClickSearcher
{
    internal class Program
    {
        const string GraphInputFileName = "graph.txt";

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            IGraphReader graphReader = new GraphFileReader(GraphInputFileName);
            long[] graph;
            try
            {
                graph = graphReader.ReadGraph();
            }
            catch (FormatException)
            {
                Console.WriteLine("Граф задан в неверном формате. Пожалуйста, задайте граф матрицей смежности.");
                return;
            }
            catch (IOException)
            {
                Console.WriteLine($"Ошибка при чтении графа с файла {GraphInputFileName}.");
                return;
            }
            catch
            {
                Console.WriteLine("Что-то пошло не так... Попробуйте еще раз.");
                return;
            }

            if (!GraphUtils.IsUndirectedGraph(graph))
            {
                Console.WriteLine("На вход ожидался неориентированный граф.");
                return;
            }

            var clickSearcher = new GraphClickSearcher();
            var maxClick = clickSearcher.GetMaxClick(graph);

            Console.WriteLine("Максимальная клика состоит из вершин: ");
            Console.WriteLine(string.Join(" ", maxClick));
        }
    }
}
