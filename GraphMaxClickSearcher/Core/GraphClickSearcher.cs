using System.Collections.Generic;

namespace GraphMaxClickSearcher
{
    public class GraphClickSearcher
    {
        public int[] GetMaxClick(long[] graph)
        {
            (var clickMask, _) = GetMaxClickInternal(graph);

            var result = new List<int>();

            var vertexMask = 1L;
            for (int i = 0; i < graph.Length; i++, vertexMask <<= 1)
            {
                if ((clickMask & vertexMask) != 0)
                {
                    result.Add(i);
                }
            }

            return result.ToArray();
        }

        private (long clickMask, int cntVertex) GetMaxClickInternal(long[] graph, long currentClickMask = 0, int cntVertex = 0, int currentIndex = 0)
        {
            var result = (currentClickMask, cntVertex);
            var indexMask = 1L << currentIndex;
            for (int i = currentIndex; i < graph.Length; i++, indexMask <<= 1)
            {
                if ((currentClickMask & graph[i]) == currentClickMask)
                {
                    var potentialMaxClick = GetMaxClickInternal(graph, currentClickMask | indexMask, cntVertex + 1, i + 1);

                    if (potentialMaxClick.cntVertex > result.cntVertex)
                    {
                        result = potentialMaxClick;
                    }
                }
            }

            return result;
        }
    }
}
