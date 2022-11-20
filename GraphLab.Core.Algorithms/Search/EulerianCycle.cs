using GraphLab.Core.Algorithms.Utils;
using System.Collections.Generic;
using System.Linq;
using System;

namespace GraphLab.Core.Algorithms.Search
{
    public class EulerianCycle<TVertex, TEdge>
        where TVertex : class, IVertex, new()
        where TEdge : IEdge<TVertex>, new()
    {
        public Graph<TVertex, TEdge> Graph { get; private set; }
        private List<TVertex> C { get; set; }
        private List<TVertex> allVert { get; set; }
        private Stack<TVertex> S { get; set; }
        private List<TEdge> notUsedEdges { get; set; }


        public EulerianCycle(Graph<TVertex, TEdge> graph)
        {
            Graph = graph;
            allVert = new();
            C = new();
        }

        public List<TVertex> Begin()
        {
            S = new();
            S.Push(Graph.Verticies.First());

            foreach (var vert in Graph.Verticies)
                allVert.Add(vert);

            notUsedEdges = new();
            foreach (var edge in Graph.Edges)
                notUsedEdges.Add(edge);

            while (S.Count != 0)
            {
                TVertex from = S.Peek();
                List<TEdge> nUEdgeForFrom = new();
                foreach (var vert in Graph[from])
                {
                    TEdge edge = notUsedEdges.Find(x => x.From == from && x.To == vert);
                    TEdge edge2 = notUsedEdges.Find(x => x.From == vert && x.To == from);
                    if (edge != null && edge2 != null)
                        nUEdgeForFrom.Add(edge);
                }

                if(nUEdgeForFrom.Count == 0)
                {
                    from = S.Pop();
                    C.Add(from);
                }
                else
                {
                    S.Push(nUEdgeForFrom[0].To);
                    TEdge edge2 = notUsedEdges.Find(x => x.From == nUEdgeForFrom[0].To && x.To == nUEdgeForFrom[0].From);
                    notUsedEdges.Remove(nUEdgeForFrom[0]);
                    notUsedEdges.Remove(edge2);
                }
            }

            return C;
        }
    }
}
