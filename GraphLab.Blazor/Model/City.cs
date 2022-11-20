using GraphLab.Core;
using System;

namespace GraphLab.Blazor.Model
{
    public class City : IVertex
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Lenght { get; set; }
    }
}
