using GraphLab.Core.Algorithms.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLab.Core.Algorithms.Search
{
    public class StrongCompAlg<TVertex, TEdge>
        where TVertex : class, IVertex, new()
        where TEdge : IEdge<TVertex>, new()
    {
        public StateManager<TVertex, SearchState> States { get; private set; }

        public StrongCompAlg()
        {
            States = new StateManager<TVertex, SearchState>(SearchState.None);
        }

        public List<TVertex[]> Search(Graph<TVertex, TEdge> Graph)
        {
            List<TVertex[]> Comps = new List<TVertex[]>();

            foreach(var v in Graph.Verticies)
            {
                if(States[v] == SearchState.None)
                {
                    TVertex[] verMas = Array.Empty<TVertex>();
                    Step(v, ref verMas, Graph);
                    Comps.Add(verMas);
                }
            }

            return Comps;
        }

        private void Step(TVertex vertex, ref TVertex[] verMas, Graph<TVertex, TEdge> Graph)
        {
            Array.Resize(ref verMas, verMas.Length + 1);
            States[vertex] = SearchState.Visited;
            verMas[^1] = vertex; 

            foreach(var v in Graph[vertex])
            {
                if(States[v] == SearchState.None)
                {
                    Step(v, ref verMas, Graph);
                }
            }   
        }

    }
}
