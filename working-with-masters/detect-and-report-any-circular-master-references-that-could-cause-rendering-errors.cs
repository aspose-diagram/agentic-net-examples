using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Build a graph of master references: master name -> list of referenced master names
            var masterGraph = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

            // Populate graph nodes
            foreach (Master master in diagram.Masters)
            {
                // Ensure entry exists even if there are no outgoing edges
                if (!masterGraph.ContainsKey(master.Name))
                    masterGraph[master.Name] = new List<string>();

                // Examine each shape inside the master for references to other masters
                foreach (Shape shape in master.Shapes)
                {
                    // Some shapes may reference another master via the Master property
                    // Guard against null (shape may be a primitive shape without a master)
                    if (shape.Master != null && !string.IsNullOrEmpty(shape.Master.Name))
                    {
                        string targetMasterName = shape.Master.Name;

                        // Add edge from current master to the referenced master
                        if (!masterGraph[master.Name].Contains(targetMasterName))
                            masterGraph[master.Name].Add(targetMasterName);
                    }
                }
            }

            // Detect cycles using depth‑first search
            var visited = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var recursionStack = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var cycles = new List<List<string>>();

            foreach (var node in masterGraph.Keys)
            {
                if (!visited.Contains(node))
                {
                    DetectCyclesDFS(node, masterGraph, visited, recursionStack, new List<string>(), cycles);
                }
            }

            // Report results
            if (cycles.Count == 0)
            {
                Console.WriteLine("No circular master references found.");
            }
            else
            {
                Console.WriteLine("Circular master references detected:");
                int count = 1;
                foreach (var cycle in cycles)
                {
                    Console.WriteLine($"{count}: {string.Join(" -> ", cycle)} -> {cycle[0]}");
                    count++;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Recursive DFS helper to find cycles
    private static void DetectCyclesDFS(
        string current,
        Dictionary<string, List<string>> graph,
        HashSet<string> visited,
        HashSet<string> recursionStack,
        List<string> path,
        List<List<string>> cycles)
    {
        visited.Add(current);
        recursionStack.Add(current);
        path.Add(current);

        if (graph.TryGetValue(current, out List<string> neighbours))
        {
            foreach (var neighbour in neighbours)
            {
                if (!visited.Contains(neighbour))
                {
                    DetectCyclesDFS(neighbour, graph, visited, recursionStack, path, cycles);
                }
                else if (recursionStack.Contains(neighbour))
                {
                    // Cycle detected – extract the cycle path
                    int startIndex = path.IndexOf(neighbour);
                    if (startIndex >= 0)
                    {
                        var cycle = path.GetRange(startIndex, path.Count - startIndex);
                        cycles.Add(cycle);
                    }
                }
            }
        }

        // Backtrack
        recursionStack.Remove(current);
        path.RemoveAt(path.Count - 1);
    }
}