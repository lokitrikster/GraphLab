using GraphLab.Core.Algorithms.Utils;
using System.Collections.Generic;
using System.Linq;

namespace GraphLab.Core.Algorithms.Search
{
    public class AlgFloydWarshell<TVertex, TEdge>
        where TVertex : class, IVertex, new()
        where TEdge : IEdge<TVertex>, new()
    {
        public Graph<TVertex, TEdge> Graph { get; private set; }
        public StateManager<TVertex, SearchState> States { get; private set; }
        private int[,] lenghtMatr { get; set; }
        public int [,] next { get; set; }
        private List<TVertex> allVert { get; set; }


        public AlgFloydWarshell(Graph<TVertex, TEdge> graph)
        {
            Graph = graph;
            States = new StateManager<TVertex, SearchState>(SearchState.None);
            lenghtMatr = new int[Graph.Verticies.Count, Graph.Verticies.Count];
            next = new int[Graph.Verticies.Count, Graph.Verticies.Count];
            allVert = new();
            foreach(var vert in Graph.Verticies) {
                allVert.Add(vert);
            }
            Graph.Verticies.First().Lenght = 0;
        }

        public int[,] Begin()
        {
            for (int i = 0; i < Graph.Verticies.Count; i++)
                for (int j = 0; j < Graph.Verticies.Count; j++)
                {
                    lenghtMatr[i, j] = (i == j) ? 0 : int.MaxValue;
                    next[i, j] = (i == j) ? i : 0;
                }

            foreach (var edge in Graph.Edges)
            {
                int k = allVert.IndexOf(edge.From);
                int m = allVert.IndexOf(edge.To);
                lenghtMatr[k, m] = edge.Price;
                next[k, m] = m;
            }

            for (int k = 0; k < Graph.Verticies.Count; k++)
                for (int i = 0; i < Graph.Verticies.Count; i++)
                    for (int j = 0; j < Graph.Verticies.Count; j++)
                        if (lenghtMatr[i, j] > lenghtMatr[i, k] + lenghtMatr[k, j] && lenghtMatr[i,k] != int.MaxValue && lenghtMatr[k,j] != int.MaxValue && k != i && k != j && i != j)
                        {
                            int a = lenghtMatr[i, k];
                            int b = lenghtMatr[k, j];
                            lenghtMatr[i, j] = a + b;
                            int c = lenghtMatr[i, j];
                            int l = next[i, k];
                            next[i, j] = l;
                        }

            return lenghtMatr;
        }
    }
}
