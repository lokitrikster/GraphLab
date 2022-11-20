using System;

namespace GraphLab.Core
{
    /// <summary>
    /// Обобщенное ребро
    /// </summary>
    /// <typeparam name="TVertex">Тип веришины</typeparam>
    public interface IEdge <TVertex>
        where TVertex : class, IVertex
    {
        Guid Id { get; set; }
        TVertex From { get; set; }
        TVertex To { get; set; }

        int Price { get; set; }
    }
}
