using Bogus;
using GraphLab.Core;
using GraphLab.Core.Algorithms.Utils;
using System.Collections.Generic;
using System.Linq;
using System;

namespace GraphLab.Blazor.Model
{
    /// <summary>
    /// Модель данных
    /// </summary>
    public class ModelService
    {
        public Graph<City, Road> Graph { get; private set; }

        public ModelService()
        {
            Graph = new Graph<City, Road>();
        }

        public void GenerateEulerianFraph()
        {
            Graph.Clear();
            for(int i = 0; i < 6; i++)
            {
                var vertex = Graph.CreateVertex();
                vertex.Name = Convert.ToString(i + 1);
            }

            List<City> allVertex = new();
            foreach (var vertex in Graph.Verticies)
                allVertex.Add(vertex);

            Graph.CreateNeorEdge(allVertex[0], allVertex[1], 1);
            Graph.CreateNeorEdge(allVertex[1], allVertex[2], 1);
            Graph.CreateNeorEdge(allVertex[2], allVertex[3], 1);
            Graph.CreateNeorEdge(allVertex[3], allVertex[4], 1);
            Graph.CreateNeorEdge(allVertex[4], allVertex[0], 1);
            Graph.CreateNeorEdge(allVertex[1], allVertex[3], 1);
            Graph.CreateNeorEdge(allVertex[1], allVertex[5], 1);
            Graph.CreateNeorEdge(allVertex[2], allVertex[5], 1);
            Graph.CreateNeorEdge(allVertex[2], allVertex[4], 1);
            Graph.CreateNeorEdge(allVertex[5], allVertex[3], 1);
            Graph.CreateNeorEdge(allVertex[5], allVertex[4], 1);
        }

        public void Generate(int vertexCount, bool orientir, int min, int max)
        {
            Graph.Clear();

            var faker = new Faker("ru");

            for (int i = 0; i < vertexCount; i++)
            {
                var vertex = Graph.CreateVertex();
                vertex.Name = faker.Address.City();
            }

            foreach (var from in Graph.Verticies)
            {
                foreach (var to in Graph.Verticies)
                {
                    if (from != to && faker.Random.Double() < 0.3)
                    {
                        List<Road> l = new();
                        foreach (var val in Graph.Edges)
                        {
                            l.Add(val);
                        }
                        int p = faker.Random.Int(min: min, max: max);

                        if (orientir)
                        {
                            var res = l.FindAll(value => (value.From == from && value.To == to));
                            if (res.Count == 0)
                            {
                                var edge = Graph.CreateEdge(from, to);
                                edge.Price = p;
                            }
                        }
                        else
                        {
                            var res = l.FindAll(value => ((value.From == to && value.To == from) || (value.From == from && value.To == to)));
                            if (res.Count == 0)
                            {
                                var edge = Graph.CreateEdge(from, to);
                                edge.Price = p;
                                var edge2 = Graph.CreateEdge(to, from);
                                edge2.Price = p;
                            }
                        }
                    }
                }
            }
        }
    }
}
