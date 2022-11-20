using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphLab.Core
{
    /// <summary>
    /// Обобщенный граф
    /// </summary>
    /// <typeparam name="TVertex">Тип вершины</typeparam>
    /// <typeparam name="TEdge">Тип ребра</typeparam>
    public class Graph<TVertex, TEdge>
        where TVertex : class, IVertex, new()
        where TEdge : IEdge<TVertex>, new()
    {
        public ISet<TVertex> Verticies { get; private set; }
        public ISet<TEdge> Edges { get; private set; }

        public Graph()
        {
            Verticies = new HashSet<TVertex>();
            Edges = new HashSet<TEdge>();
        }

        /// <summary>
        /// Создание вершины
        /// </summary>
        /// <returns></returns>
        public TVertex CreateVertex()
        {
            var vertex = new TVertex { Id = System.Guid.NewGuid(), Lenght = int.MaxValue };
            Verticies.Add(vertex);
            return vertex;
        }

        /// <summary>
        /// Создание ребра
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public TEdge CreateEdge(TVertex from, TVertex to)
        {
            var edge = new TEdge
            {
                From = from,
                To = to,
                Id = Guid.NewGuid(),
            };
            Edges.Add(edge);
            return edge;
        }

        public void CreateNeorEdge(TVertex from, TVertex to, int price)
        {
            var edge = new TEdge
            {
                From = from,
                To = to,
                Id = Guid.NewGuid(),
                Price = price,
            };

            var edge2 = new TEdge
            {
                From = to,
                To = from,
                Id = Guid.NewGuid(),
                Price = price,
            };

            Edges.Add(edge);
            Edges.Add(edge2);
        }

        /// <summary>
        /// Очистка графа
        /// </summary>
        public void Clear()
        {
            Edges.Clear();
            Verticies.Clear();
        }

        /// <summary>
        /// Список смежных с вершиной вершин
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public ISet<TVertex> this[TVertex vertex]
        {
            get => Edges
                .Where(x => x.From == vertex)
                .Select(x => x.To)
                .Distinct()
                .ToHashSet();
        }

        public int[,] AdjacencyMatrix()
        {
            int[,] res = new int[Verticies.Count, Verticies.Count];
            
            int i = 0;
            foreach(var vert in Verticies)
            {
                foreach(var adj in this[vert])
                {
                    int j = 0;
                    foreach (var v in Verticies)
                    {
                        if(adj.Id == v.Id) 
                            res[i, j] = 1;
                        j++;
                    }
                }
                i++;
            }
                  
            return res;
        }

        public int[,] ReachabilityMatrix()
        {
            int[,] res = AdjacencyMatrix();
            int N = Verticies.Count;

            for (int k = 0; k < N; k++)
                for (int i = 0; i < N; i++)
                    for (int j = 0; j < N; j++)
                        res[i,j] = (res[i,j] | (res[i,k] & res[k,j]));

            return res;
        }
    }
}
