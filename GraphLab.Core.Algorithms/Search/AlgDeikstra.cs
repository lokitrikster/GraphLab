using GraphLab.Core.Algorithms.Utils;
using System.Collections.Generic;
using System.Linq;

namespace GraphLab.Core.Algorithms.Search
{
    public class AlgDeikstra<TVertex, TEdge> : ISearchAlgorithm<TVertex, TEdge>
        where TVertex : class, IVertex, new()
        where TEdge : IEdge<TVertex>, new()
    {
        public Graph<TVertex, TEdge> Graph { get; private set; }
        public TVertex StartVertex { get; private set; }
        public StateManager<TVertex, SearchState> States { get; private set; }
        public List<TVertex> visitedV { get; set; }
        public List<TVertex> nVisitedV { get; set; }


        public AlgDeikstra(Graph<TVertex, TEdge> graph, TVertex startVertex)
        {
            Graph = graph;
            StartVertex = startVertex;
            States = new StateManager<TVertex, SearchState>(SearchState.None);
            visitedV = new();
            nVisitedV = new();
        }

        public AlgorithmStep Begin()
        {
            foreach (var vert in Graph.Verticies)
                nVisitedV.Add(vert);
            nVisitedV[0].Lenght = 0;

            return Step();
        }

        private AlgorithmStep Step()
        {
            if (nVisitedV.Count == 0) return null;
            
            nVisitedV.Sort((x, y) => x.Lenght.CompareTo(y.Lenght));
            
            List<TEdge> nearestE = new();
            foreach (var edge in Graph.Edges)
                if (edge.From == nVisitedV[0] && !(visitedV.Contains(edge.To)))
                    nearestE.Add(edge);
            nearestE.Sort((x, y) => x.Price.CompareTo(y.Price));

            foreach(var edge in nearestE)
            {
                int sumLenght = edge.From.Lenght + edge.Price;
                if (edge.To.Lenght > sumLenght)
                    edge.To.Lenght = sumLenght;
            }

            visitedV.Add(nVisitedV[0]);
            nVisitedV.RemoveAt(0);
            MakeCurrent(visitedV.Last());

            return Step;
        }

        private void MakeCurrent(TVertex vertex)
        {
            var currentVertex = Graph.Verticies.SingleOrDefault(
                x => States[x] == SearchState.Current);

            if (currentVertex != null)
                States[currentVertex] = SearchState.Visited;

            States[vertex] = SearchState.Current;
        }
    }
}
