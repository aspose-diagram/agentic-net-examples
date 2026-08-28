using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file; can be passed as a command‑line argument
        string filePath = args.Length > 0 ? args[0] : "input.vsdx";

        Diagram diagram;
        try
        {
            diagram = new Diagram(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        bool anyCircular = false;

        // Examine each master in the document
        foreach (Master master in diagram.Masters)
        {
            var visited = new HashSet<string>();
            if (HasCircularReference(master, diagram, visited))
            {
                Console.WriteLine($"Circular master reference detected starting at master '{master.Name}'.");
                anyCircular = true;
            }
        }

        if (!anyCircular)
        {
            Console.WriteLine("No circular master references found.");
        }
    }

    // Recursive depth‑first search to detect cycles among masters
    static bool HasCircularReference(Master current, Diagram diagram, HashSet<string> visited)
    {
        if (current == null) return false;

        // If we have already visited this master, a cycle exists
        if (visited.Contains(current.Name))
            return true;

        visited.Add(current.Name);

        // Inspect each shape contained in the current master
        foreach (Shape shape in current.Shapes)
        {
            // Shapes may be instances of other masters
            Master? referenced = shape.Master;
            if (referenced != null)
            {
                // Resolve the referenced master from the diagram collection
                Master? target = diagram.Masters.GetMasterByName(referenced.Name);
                if (target != null)
                {
                    // Recurse with a copy of the visited set to keep path state
                    if (HasCircularReference(target, diagram, new HashSet<string>(visited)))
                        return true;
                }
            }
        }

        return false;
    }
}
