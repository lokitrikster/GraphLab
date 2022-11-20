using GraphLab.Core.Algorithms.Utils;
using System.Collections.Generic;
using System.Linq;
using System;

namespace GraphLab.Core.Algorithms.Search
{
    public class AlgBellmanFord<TVertex, TEdge>
        where TVertex : class, IVertex, new()
        where TEdge : IEdge<TVertex>, new()
    {
        public Graph<TVertex, TEdge> Graph { get; private set; }
        public List<List<int>> lenght { get; set; }
        public List<TVertex> allVert { get; set; }


        public AlgBellmanFord(Graph<TVertex, TEdge> graph)
        {
            Graph = graph;
            allVert = new();
            Graph.Verticies.First().Lenght = 0;
        }

        public List<List<int>> Begin()
        {
            lenght = new();
            allVert = Graph.Verticies.ToList();

            List<int> d = new();
            for (int i = 0; i < allVert.Count; i++)
                d.Add((i == 0) ? 0 : int.MaxValue);

            for (int i = 0; i < allVert.Count - 1; i++)
            {
                foreach (var edge in Graph.Edges)
                {
                    if (d[allVert.IndexOf(edge.From)] < int.MaxValue)
                    {
                        d[allVert.IndexOf(edge.To)] = Math.Min(d[allVert.IndexOf(edge.To)], d[allVert.IndexOf(edge.From)] + edge.Price);
                        edge.To.Lenght = d[allVert.IndexOf(edge.To)];
                    }
                }

                List<int> h = new();
                foreach (var v in d)
                    h.Add(v);

                lenght.Add(h);
            }

            return lenght;
        }
    }
}
