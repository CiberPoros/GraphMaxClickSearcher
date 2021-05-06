using System;
using System.Collections.Generic;
using System.IO;

namespace GraphMaxClickSearcher.GraphIO
{
    internal class GraphFileReader : IGraphReader
    {
        private static readonly Dictionary<string, bool> _vertexAbleValues = new()
        {
            { "0", false },
            { "1", true }
        };

        private readonly string _fileName;

        public GraphFileReader(string fileName)
        {
            _fileName = fileName;
        }

        public long[] ReadGraph()
        {
            var result = new List<long>();

            var vertexCount = -1;
            foreach (var str in File.ReadLines(_fileName))
            {
                var vertexes = str.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (vertexCount == -1)
                {
                    vertexCount = vertexes.Length;
                }

                if (vertexes.Length != vertexCount)
                {
                    throw new FormatException("Invalid graph format.");
                }

                var mask = 0L;
                var currentValue = 1L;
                foreach (var vertex in vertexes)
                {
                    if (!_vertexAbleValues.TryGetValue(vertex, out var isConnected))
                    {
                        throw new FormatException("Invalid graph format.");
                    }

                    if (isConnected)
                    {
                        mask |= currentValue;
                    }

                    currentValue <<= 1;
                }

                result.Add(mask);
            }

            return result.ToArray();
        }
    }
}
