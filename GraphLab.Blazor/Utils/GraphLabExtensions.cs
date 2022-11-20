using GraphLab.Blazor.Model;
using GraphLab.Core;
using GraphLab.Core.Algorithms.Search;
using Microsoft.JSInterop;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using System.Drawing;
using System;
using System.Collections.Generic;

namespace GraphLab.Blazor.Utils
{
    public static class GraphLabExtensions
    {
        /// <summary>
        /// Первоночальное создание визуального графа с использованием
        /// библиотеки VisJS
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <param name="graph"></param>
        /// <returns></returns>
        public static async Task BindGraph(this IJSRuntime jsRuntime, Graph<City, Road> graph, bool orientir)
        {
            var vertecies = graph.Verticies
                .Select(x => new
                {
                    id = x.Id,
                    label = x.Name,
                    color = "lightblue"
                }).ToArray();

            if (orientir)
            {
                var edges = graph.Edges
                    .Select(x => new
                    {
                        id = x.Id,
                        from = x.From.Id,
                        to = x.To.Id,
                        arrows = "to",
                        label = x.Price.ToString()
                    }).ToArray();

                await jsRuntime.InvokeVoidAsync("bindGraph", vertecies, edges);
            }
            else
            {
                var edges = graph.Edges
                    .Select(x => new
                    {
                        id = x.Id,
                        from = x.From.Id,
                        to = x.To.Id,
                        line = "to",
                        label = x.Price.ToString()
                    }).ToArray();

                await jsRuntime.InvokeVoidAsync("bindGraph", vertecies, edges);
            }
        }

        /// <summary>
        /// Обновленние вершины в визуальном графе
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <param name="vertex"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static async Task UpdateVertex(this IJSRuntime jsRuntime, City vertex, SearchState state, bool lenghtV)
        {
            if(!lenghtV)
            {
                await jsRuntime.InvokeVoidAsync("updateVertex",
                new
                {
                    id = vertex.Id,
                    label = vertex.Name,
                    color = state switch
                    {
                        SearchState.Current => "pink",
                        SearchState.Visited => "lightgreen",
                        _ => "lightblue"
                    }
                });
            }
            else
            {
                string lenght = (vertex.Lenght < -1000 || vertex.Lenght > 1000) ? vertex.Name + " ..." : vertex.Name + " " + vertex.Lenght;
                await jsRuntime.InvokeVoidAsync("updateVertex",
                new
                {
                    id = vertex.Id,
                    label = lenght,
                    color = state switch
                    {
                        SearchState.Current => "pink",
                        SearchState.Visited => "lightgreen",
                        _ => "lightblue"
                    }
                });
            }
        }

        public static async Task UpdateEdge(this IJSRuntime jsRuntime, Road ed, string color, bool orientir)
        {
            if (orientir)
            {
                await jsRuntime.InvokeVoidAsync("updateEdge",
                   new
                   {
                       id = ed.Id,
                       from = ed.From.Id,
                       to = ed.To.Id,
                       arrow = "to",
                       label = ed.Price.ToString(),
                       color,
                   });
            }
            else
            {
                await jsRuntime.InvokeVoidAsync("updateEdge",
                   new
                   {
                       id = ed.Id,
                       from = ed.From.Id,
                       to = ed.To.Id,
                       line = "to",
                       label = ed.Price.ToString(),
                       color,
                   });
            }
        }

        public static async Task PainEulerianCycle (this IJSRuntime jsRuntime, Road fromStart, Road toStart)
        {
            await jsRuntime.InvokeVoidAsync("updateEdge",
                   new
                   {
                       id = fromStart.Id,
                       from = fromStart.From.Id,
                       to = fromStart.To.Id,
                       arrows = "to",
                       line = "to",
                       label = fromStart.Price.ToString(),
                       color = "lightgreen",
                   });

            await jsRuntime.InvokeVoidAsync("updateEdge",
                   new
                   {
                       id = toStart.Id,
                       from = toStart.From.Id,
                       to = toStart.To.Id,
                       line = "to",
                       label = toStart.Price.ToString(),
                       color = "lightgreen",
                   });
        }

        public static async Task UpdateStrongComp(this IJSRuntime jsRuntime, List<City[]> cities)
        {
            for (int i = 0; i < cities.Count; i++)
            {
                foreach (var c in cities[i])
                {
                    await jsRuntime.InvokeVoidAsync("updateVertex",
                    new
                    {
                        id = c.Id,
                        label = c.Name,
                        color = i switch
                        {
                            0 => "green",
                            1 => "pink",
                            2 => "yellow",
                            3 => "blue",
                            4 => "lightblue",
                            5 => "lightgreen",
                            _ => "red"
                        }
                    });
                }
            }
        }
    }
}
