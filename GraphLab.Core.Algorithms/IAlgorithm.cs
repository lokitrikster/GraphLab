namespace GraphLab.Core.Algorithms
{
    /// <summary>
    /// Шаг алгоритма: функция которая что-то делает 
    /// и возвращает фцнкцию - следующий шаг алгоритма
    /// </summary>
    /// <returns></returns>
    public delegate AlgorithmStep AlgorithmStep();

    /// <summary>
    /// Пошаговый алгоритм на графах
    /// </summary>
    /// <typeparam name="TVertex"></typeparam>
    /// <typeparam name="TEdge"></typeparam>
    public interface IAlgorithm<TVertex, TEdge>
        where TVertex : class, IVertex, new()
        where TEdge : IEdge<TVertex>, new()
    {
        /// <summary>
        /// Граф
        /// </summary>
        Graph<TVertex, TEdge> Graph { get; }

        /// <summary>
        /// Старт алгоритма
        /// </summary>
        /// <returns></returns>
        AlgorithmStep Begin();
    }
}
