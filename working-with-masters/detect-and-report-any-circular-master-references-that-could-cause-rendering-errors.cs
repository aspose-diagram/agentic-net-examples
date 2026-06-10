using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class CircularMasterDetector
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            string diagramPath = "input.vsdx";
            Diagram diagram = new Diagram(diagramPath);

            // Build a graph where each master ID points to the master IDs it references via its shapes
            var masterGraph = new Dictionary<int, List<int>>();

            foreach (Master master in diagram.Masters)
            {
                int masterId = master.ID;
                if (!masterGraph.ContainsKey(masterId))
                    masterGraph[masterId] = new List<int>();

                // Examine each shape within the master
                foreach (Shape shape in master.Shapes)
                {
                    // If the shape is based on another master, add an edge
                    if (shape.Master != null)
                    {
                        int referencedMasterId = shape.Master.ID;
                        masterGraph[masterId].Add(referencedMasterId);
                    }
                }
            }

            // Detect cycles using depth‑first search
            var visited = new HashSet<int>();
            var recursionStack = new HashSet<int>();
            var cycles = new List<List<int>>();

            foreach (int node in masterGraph.Keys)
            {
                if (!visited.Contains(node))
                    DFS(node, masterGraph, visited, recursionStack, new List<int>(), cycles);
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
                    Console.WriteLine($"{count}: {string.Join(" -> ", cycle)} -> {cycle[0]}");
                    count++;
                }
            }

            // Clean up
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Depth‑first search that records cycles
    private static void DFS(
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

        if (graph.TryGetValue(current, out List<int> neighbours))
        {
            foreach (int neighbour in neighbours)
            {
                if (!visited.Contains(neighbour))
                {
                    DFS(neighbour, graph, visited, recursionStack, path, cycles);
                }
                else if (recursionStack.Contains(neighbour))
                {
                    // Cycle detected – extract the cycle path
                    int index = path.IndexOf(neighbour);
                    if (index != -1)
                    {
                        var cycle = new List<int>();
                        for (int i = index; i < path.Count; i++)
                            cycle.Add(path[i]);
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
