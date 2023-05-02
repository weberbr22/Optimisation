using System;
using System.Collections.Generic;

namespace Graphs
{
    internal class Graph
    {
        private int V;
        private List<Tuple<int, int>>[] adj;

        public Graph(int v)
        {
            V = v;
            adj = new List<Tuple<int, int>>[V];
            for (int i = 0; i < V; i++)
            {
                adj[i] = new List<Tuple<int, int>>();
            }
        }

        public void AddEdge(int u, int v, int weight)
        {
            adj[u].Add(new Tuple<int, int>(v, weight));
            adj[v].Add(new Tuple<int, int>(u, weight));
        }

        public int[] Dijkstra(int src)
        {
            int[] dist = new int[V];
            bool[] visited = new bool[V];

            for (int i = 0; i < V; i++)
            {
                dist[i] = int.MaxValue;
            }

            dist[src] = 0;

            for (int i = 0; i < V - 1; i++)
            {
                int u = MinDistance(dist, visited);
                visited[u] = true;

                foreach (var edge in adj[u])
                {
                    int v = edge.Item1;
                    int weight = edge.Item2;

                    if (!visited[v] && dist[u] != int.MaxValue && dist[u] + weight < dist[v])
                    {
                        dist[v] = dist[u] + weight;
                    }
                }
            }

            return dist;
        }

        private int MinDistance(int[] dist, bool[] visited)
        {
            int minDist = int.MaxValue;
            int minIndex = -1;

            for (int i = 0; i < V; i++)
            {
                if (!visited[i] && dist[i] <= minDist)
                {
                    minDist = dist[i];
                    minIndex = i;
                }
            }

            return minIndex;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Graph g = new Graph(5);

            g.AddEdge(0, 1, 4);
            g.AddEdge(0, 2, 2);
            g.AddEdge(1, 2, 1);
            g.AddEdge(1, 3, 5);
            g.AddEdge(2, 3, 8);
            g.AddEdge(2, 4, 10);
            g.AddEdge(3, 4, 6);

            int[] dist = g.Dijkstra(0);

            for (int i = 0; i < dist.Length; i++)
            {
                Console.WriteLine("Shortest distance from vertex 0 to vertex " + i + " is " + dist[i]);
            }
        }
    }
}
