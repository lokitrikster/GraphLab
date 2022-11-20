using GraphLab.Core.Algorithms.Utils;

namespace GraphLab.Core.Algorithms.Search
{
    /// <summary>
    /// Обобщение алгоритма поиска (обхода графа)
    /// </summary>
    /// <typeparam name="TVertex">Тип вершины</typeparam>
    /// <typeparam name="TEdge">Тип ребра</typeparam>
    public interface ISearchAlgorithm<TVertex, TEdge> : IAlgorithm<TVertex, TEdge>
        where TVertex : class, IVertex, new()
        where TEdge : IEdge<TVertex>, new()
    {
        /// <summary>
        /// Вершина с которой начинается поиск
        /// </summary>
        TVertex StartVertex { get; }

        /// <summary>
        /// Состояния вершин
        /// </summary>
        StateManager<TVertex, SearchState> States { get; }

    }
}
