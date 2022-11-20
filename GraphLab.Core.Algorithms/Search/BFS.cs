using GraphLab.Core.Algorithms.Utils;
using System.Collections.Generic;
using System.Linq;

namespace GraphLab.Core.Algorithms.Search
{
    /// <summary>
    /// Поиск в ширину
    /// </summary>
    /// <typeparam name="TVertex"></typeparam>
    /// <typeparam name="TEdge"></typeparam>
    public class BFS<TVertex, TEdge> : ISearchAlgorithm<TVertex, TEdge>
        where TVertex : class, IVertex, new()
        where TEdge : IEdge<TVertex>, new()
    {
        public Graph<TVertex, TEdge> Graph { get; private set; }
        public TVertex StartVertex { get; private set; }

        public StateManager<TVertex, SearchState> States { get; private set; }

        public BFS(Graph<TVertex, TEdge> graph, TVertex startVertex)
        {
            Graph = graph;
            StartVertex = startVertex;
            States = new StateManager<TVertex, SearchState>(SearchState.None);
        }

        private Queue<TVertex> _bag;

        public AlgorithmStep Begin()
        {
            _bag = new Queue<TVertex>();
            _bag.Enqueue(StartVertex);

            return Step();
        }

        private AlgorithmStep Step()
        {
            if (!_bag.Any())
                return null;

            var vertex = _bag.Dequeue();
            MakeCurrent(vertex);

            foreach (var adj in Graph[vertex])
            {
                if (States[adj] == SearchState.None && !_bag.Contains(adj))
                {
                    _bag.Enqueue(adj);
                }
            }

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
