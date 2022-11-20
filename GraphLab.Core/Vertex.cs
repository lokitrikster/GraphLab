using System;

namespace GraphLab.Core
{
    /// <summary>
    /// Обобщенная вершина
    /// </summary>
    public interface IVertex
    {
        Guid Id { get; set; }
        public int Lenght { get; set; }
    }
}
