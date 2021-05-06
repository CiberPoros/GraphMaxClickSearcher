namespace GraphMaxClickSearcher
{
    public static class GraphUtils
    {
        public static bool IsUndirectedGraph(long[] graph)
        {
            var iMask = 1L;
            for (int i = 0; i < graph.Length; i++, iMask <<= 1)
            {
                var jMask = 1L << (i + 1);
                for (int j = i + 1; j < graph.Length; j++, jMask <<= 1)
                {
                    if (((graph[i] & jMask) == 0) != ((graph[j] & iMask) == 0))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
