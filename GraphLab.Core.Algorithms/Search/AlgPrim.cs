using GraphLab.Core.Algorithms.Utils;
using System.Collections.Generic;
using System.Linq;

namespace GraphLab.Core.Algorithms.Search
{
    public class AlgPrim<TVertex, TEdge>
        where TVertex : class, IVertex, new()
        where TEdge : IEdge<TVertex>, new()
    {
        public Graph<TVertex, TEdge> Graph { get; private set; }
        public TVertex StartVertex { get; private set; }

        public StateManager<TVertex, SearchState> StatesV { get; private set; }
        public StateManager<TEdge, SearchState> States { get; private set; }

        public List<TEdge> MST { get; set; }
        private List<TVertex> usedV { get; set; }
        private List<TVertex> notUsedV { get; set; }
        private List<TEdge> allEdge { get; set; }


        public AlgPrim(Graph<TVertex, TEdge> graph)
        {
            Graph = graph;
            StartVertex = graph.Verticies.First();
            States = new StateManager<TEdge, SearchState>(SearchState.None);
            StatesV = new StateManager<TVertex, SearchState>(SearchState.None);

            MST = new();

            allEdge = new();
            foreach (var ed in Graph.Edges)
                allEdge.Add(ed);

            usedV = new();
            notUsedV = new();
        }

        public List<TEdge> Begin()
        {
            usedV.Add(StartVertex);
            MakeCurrent(StartVertex);

            foreach (var vert in Graph.Verticies)
                notUsedV.Add(vert);
            notUsedV.Remove(usedV[0]);

            while(notUsedV.Count != 0)
                Step();

            return MST;
        }

        private void Step()
        {
            if (notUsedV.Count == 0)
                return;

            List<TEdge> nue = new ();
            List<TVertex> uv = usedV;
            List<TVertex> nuv = notUsedV;


            foreach (var UVert in uv)
            {
                foreach (var NUVert in Graph[UVert])
                {
                    foreach (var edge in Graph.Edges)
                    {
                        if (edge.From == UVert && edge.To == NUVert && uv.IndexOf(edge.To) == -1 && MST.IndexOf(edge) == -1)
                            nue.Add(edge);
                    }
                }
            }

            int minIdxE = 0;
            for(int i = 1; i < nue.Count; i++)
            {
                if (nue[i].Price < nue[minIdxE].Price)
                    minIdxE = i;
            }

            nuv.Remove(nue[minIdxE].To);
            uv.Add(nue[minIdxE].To);
            MakeCurrentEdge(nue[minIdxE]);
            MST.Add(allEdge.Find(x => x.To == nue[minIdxE].From && x.From == nue[minIdxE].To));
            MST.Add(nue[minIdxE]);
        }

        private void MakeCurrent(TVertex vertex)
        {
            StatesV[vertex] = SearchState.Visited;
        }

        private void MakeCurrentEdge(TEdge edge)
        {
            States[edge] = SearchState.Visited;
        }
    }
}
