using GraphLab.Core;
using System;

namespace GraphLab.Blazor.Model
{
    public class Road : IEdge<City>
    {
        public Guid Id { get; set; }
        public City From { get; set; }
        public City To { get; set; }
        public int Price { get; set; }
    }
}
