using System;
using System.Collections.Generic;
using System.Linq;

namespace BFS
{
    public class Node
    {
        public string Name { get; set; }
        public List<string> ConnectedNodes { get; set; }
        public bool IsVisited { get; set; }
    }

    public class Graph
    {
        public List<Node> Nodes { get; set; }
    }

    internal class Program
    {
        static List<string> VisitedNodes = new();

        static void Main(string[] args)
        {
            var graph = new Graph()
            {
                Nodes = new List<Node>
                {
                    new Node { Name = "A", ConnectedNodes = new List<string> { "B", "C" } },
                    new Node { Name = "B", ConnectedNodes = new List<string> { "D", "E" } },
                    new Node { Name = "C", ConnectedNodes = new List<string> { "F", "G" } },
                    new Node { Name = "D" },
                    new Node { Name = "E" },
                    new Node { Name = "F" },
                    new Node { Name = "G", ConnectedNodes = new List<string> { "B", "E" } },
                },
            };

            BFS(graph, graph.Nodes.Where(a => a.Name == "A").FirstOrDefault());

            Console.WriteLine($"BFS Visted nodes are : {String.Join(", ", VisitedNodes.Select(a => a))} ");
        }

        public static void BFS(Graph graph, Node node)
        {
            Queue<string> QueueItems = new();
            QueueItems.Enqueue(node.Name);

            while (QueueItems.Any())
            {
                var item = QueueItems.FirstOrDefault();
                VisitedNodes.Add(item);
                var connectedNodes = graph.Nodes.Where(a => a.Name == item && a.IsVisited == false).FirstOrDefault().ConnectedNodes ?? new();

                foreach (var cItem in connectedNodes)
                {
                    var cNode = graph.Nodes.FirstOrDefault(a => a.Name == cItem && !a.IsVisited);
                    if (cNode != null)
                    {
                        QueueItems.Enqueue(cItem);
                    }
                }
                
                graph.Nodes.FirstOrDefault(a => a.Name == item).IsVisited = true;
                QueueItems.Dequeue();
            }
        }
    }
}
