using GraphLab.Core.Algorithms.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLab.Core.Algorithms.Search
{
    /// <summary>
    /// Поиск в глубину
    /// </summary>
    /// <typeparam name="TVertex"></typeparam>
    /// <typeparam name="TEdge"></typeparam>
    public class DFS<TVertex, TEdge> : ISearchAlgorithm<TVertex, TEdge>
        where TVertex : class, IVertex, new()
        where TEdge : IEdge<TVertex>, new()
    {
        public Graph<TVertex, TEdge> Graph { get; private set; }
        public TVertex StartVertex { get; private set; }

        public StateManager<TVertex, SearchState> States { get; private set; }

        public DFS(Graph<TVertex, TEdge> graph, TVertex startVertex)
        {
            Graph = graph;
            StartVertex = startVertex;
            States = new StateManager<TVertex, SearchState>(SearchState.None);
        }

        private Stack<TVertex> _bag;

        public AlgorithmStep Begin()
        {
            _bag = new Stack<TVertex>();
            _bag.Push(StartVertex);

            return Step();
        }

        private AlgorithmStep Step()
        {
            if (!_bag.Any())
                return null;

            var vertex = _bag.Pop();
            MakeCurrent(vertex);

            foreach (var adj in Graph[vertex])
            {
                if (States[adj] == SearchState.None && !_bag.Contains(adj))
                {
                    _bag.Push(adj);
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
