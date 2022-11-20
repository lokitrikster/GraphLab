using GraphLab.Core.Algorithms.Utils;
using System.Collections.Generic;
using System.Linq;

namespace GraphLab.Core.Algorithms.Search
{
    public class AlgKruskal<TVertex, TEdge>
        where TVertex : class, IVertex, new()
        where TEdge : IEdge<TVertex>, new()
    {
        public Graph<TVertex, TEdge> Graph { get; private set; }
        public TVertex StartVertex { get; private set; }
        public StateManager<TVertex, SearchState> States { get; private set; }
        private List<TEdge> allEdge { get; set; }
        public List<TEdge> MST { get; set; }
        private List<List<TVertex>> usedV { get; set; }

        public AlgKruskal(Graph<TVertex, TEdge> graph)
        {
            Graph = graph;
            StartVertex = graph.Verticies.First();
            States = new StateManager<TVertex, SearchState>(SearchState.None);

            MST = new();

            allEdge = new();
            foreach (var ed in Graph.Edges)
                allEdge.Add(ed);

            usedV = new(graph.Verticies.Count);
        }

        public List<TEdge> Begin()
        {
            List<TEdge> nue = new();
            foreach (var e in Graph.Edges)
                nue.Add(e);
            nue.Sort((x, y) => x.Price.CompareTo(y.Price));

            for (int i = 0; i < nue.Count; i++)
            {
                if (!cycle(nue[i]))
                {
                    MST.Add(nue[i]);
                    MST.Add(allEdge.Find(x => x.To == nue[i].From && x.From == nue[i].To));
                    addList(nue[i]);
                    if (usedV[0].Count == Graph.Verticies.Count) i = nue.Count;
                }
            }

            return MST;
        }

        bool cycle(TEdge edge)
        {
            for (int i = 0; i < usedV.Count; i++)
            {
                if (usedV[i].IndexOf(edge.From) != -1 && usedV[i].IndexOf(edge.To) != -1)
                    return true;
            }
            return false;
        }

        private void addList(TEdge edge)
        {
            int start = -1, end = -1, idxNewL = 0;
            for (int i = 0; i < usedV.Count; i++) 
            {
                if (usedV[i].IndexOf(edge.From) != -1) start = i;
                if (usedV[i].IndexOf(edge.To) != -1) end = i;
                if (i != 0 && usedV[i] == null && usedV[i - 1] != null)
                    idxNewL = i;
            }

            if (start != -1 && end != -1) 
            {
                if (end == 0)
                {
                    foreach (var v in usedV[start])
                        usedV[end].Add(v);
                    usedV.RemoveAt(start);
                }
                else
                {
                    foreach (var v in usedV[end])
                        usedV[start].Add(v);
                    usedV.RemoveAt(end);
                }
            }
            else
            {
                if (start != -1 && end == -1)
                    usedV[start].Add(edge.To);

                if (start == -1 && end != -1)
                    usedV[end].Add(edge.From);

                if (start == -1 && end == -1) 
                {
                    List<TVertex> newL = new();
                    newL.Add(edge.From);
                    newL.Add(edge.To);
                    usedV.Add(newL);
                }
            }
        }
    }
}
