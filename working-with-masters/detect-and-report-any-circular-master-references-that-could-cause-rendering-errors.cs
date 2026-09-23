using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to analyze
            string filePath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(filePath);

            // Build a graph of master references: master ID -> list of referenced master IDs
            var masterGraph = new Dictionary<int, List<int>>();
            var masterIdToName = new Dictionary<int, string>();

            foreach (Master master in diagram.Masters)
            {
                int masterId = master.ID;
                masterIdToName[masterId] = master.Name ?? $"Master_{masterId}";
                var references = new List<int>();

                // Examine each shape within the master
                foreach (Shape shape in master.Shapes)
                {
                    // If the shape itself is based on another master, record the reference
                    if (shape.Master != null)
                    {
                        int referencedId = shape.Master.ID;
                        references.Add(referencedId);
                    }
                }

                masterGraph[masterId] = references;
            }

            // Detect cycles using DFS
            var visited = new HashSet<int>();
            var recursionStack = new HashSet<int>();
            var cycles = new List<List<int>>();

            foreach (int masterId in masterGraph.Keys)
            {
                if (!visited.Contains(masterId))
                {
                    DetectCycles(masterId, masterGraph, visited, recursionStack, new List<int>(), cycles);
                }
            }

            // Report results
            if (cycles.Count == 0)
            {
                Console.WriteLine("No circular master references detected.");
            }
            else
            {
                Console.WriteLine("Circular master references found:");
                int count = 1;
                foreach (var cycle in cycles)
                {
                    Console.Write($"Cycle {count}: ");
                    for (int i = 0; i < cycle.Count; i++)
                    {
                        int id = cycle[i];
                        Console.Write(masterIdToName[id]);
                        if (i < cycle.Count - 1) Console.Write(" -> ");
                    }
                    Console.WriteLine();
                    count++;
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    static void DetectCycles(
        int current,
        Dictionary<int, List<int>> graph,
        HashSet<int> visited,
        HashSet<int> recursionStack,
        List<int> path,
        List<List<int>> cycles)
    {
        visited.Add(current);
        recursionStack.Add(current);
        path.Add(current);

        if (graph.TryGetValue(current, out List<int> neighbors))
        {
            foreach (int neighbor in neighbors)
            {
                if (!visited.Contains(neighbor))
                {
                    DetectCycles(neighbor, graph, visited, recursionStack, path, cycles);
                }
                else if (recursionStack.Contains(neighbor))
                {
                    // Cycle detected - extract the cycle path
                    int startIndex = path.IndexOf(neighbor);
                    if (startIndex != -1)
                    {
                        var cycle = new List<int>();
                        for (int i = startIndex; i < path.Count; i++)
                        {
                            cycle.Add(path[i]);
                        }
                        cycle.Add(neighbor); // close the loop
                        cycles.Add(cycle);
                    }
                }
            }
        }

        recursionStack.Remove(current);
        path.RemoveAt(path.Count - 1);
    }
}
