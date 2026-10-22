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
            Console.Error.WriteLine("Usage: Program <diagram-file-path>");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        Diagram diagram;
        try
        {
            // Load the Visio diagram
            diagram = new Diagram(diagramPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Build a dependency graph where each master points to masters it references via its shapes
        var masterGraph = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

        try
        {
            foreach (Master master in diagram.Masters)
            {
                string masterName = master.Name ?? string.Empty;
                var dependencies = new List<string>();

                // Iterate shapes inside the master to find referenced masters
                foreach (Shape shape in master.Shapes)
                {
                    // If the shape itself is based on another master, record the dependency
                    if (shape.Master != null)
                    {
                        string referencedName = shape.Master.Name ?? string.Empty;
                        if (!string.IsNullOrEmpty(referencedName) && !referencedName.Equals(masterName, StringComparison.OrdinalIgnoreCase))
                        {
                            dependencies.Add(referencedName);
                        }
                        else if (referencedName.Equals(masterName, StringComparison.OrdinalIgnoreCase))
                        {
                            // Self‑reference is also a circular reference
                            dependencies.Add(referencedName);
                        }
                    }
                }

                masterGraph[masterName] = dependencies;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while building master dependency graph: {ex.Message}");
            return;
        }

        // Detect cycles in the master dependency graph using DFS
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
            Console.WriteLine("No circular master references detected.");
        }
        else
        {
            Console.WriteLine("Circular master references found:");
            int cycleIndex = 1;
            foreach (var cycle in cycles)
            {
                Console.WriteLine($"Cycle {cycleIndex}: {string.Join(" -> ", cycle)}");
                cycleIndex++;
            }
        }
    }

    // Recursive DFS to find cycles; records each distinct cycle found
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
                    // Cycle detected: extract the sub‑path forming the cycle
                    int startIdx = path.IndexOf(neighbour);
                    if (startIdx >= 0)
                    {
                        var cycle = new List<string>();
                        for (int i = startIdx; i < path.Count; i++)
                        {
                            cycle.Add(path[i]);
                        }
                        cycle.Add(neighbour); // close the loop
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