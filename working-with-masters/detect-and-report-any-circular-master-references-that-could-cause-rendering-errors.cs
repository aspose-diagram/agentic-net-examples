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

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Build a lookup dictionary for masters by their unique ID
            var masterById = new Dictionary<int, Master>();
            foreach (Master master in diagram.Masters)
            {
                masterById[master.ID] = master;
            }

            // Keep track of already reported cycles to avoid duplicate output
            var reportedCycles = new HashSet<string>();

            // Check each master for circular references
            foreach (Master master in diagram.Masters)
            {
                var traversalStack = new List<int>();
                var visitedGlobal = new HashSet<int>();
                DetectCircularReference(master, masterById, visitedGlobal, traversalStack, reportedCycles);
            }

            // Save the diagram if any modifications were made (optional)
            diagram.Save("output.vsdx", SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Depth‑first search to find cycles in master references
    static void DetectCircularReference(
        Master currentMaster,
        Dictionary<int, Master> masterLookup,
        HashSet<int> visitedGlobal,
        List<int> traversalStack,
        HashSet<string> reportedCycles)
    {
        // If the current master is already in the traversal stack, a cycle exists
        if (traversalStack.Contains(currentMaster.ID))
        {
            int cycleStartIndex = traversalStack.IndexOf(currentMaster.ID);
            var cycleIds = traversalStack.GetRange(cycleStartIndex, traversalStack.Count - cycleStartIndex);
            cycleIds.Add(currentMaster.ID); // complete the loop

            string cycleKey = string.Join(" -> ", cycleIds);
            if (!reportedCycles.Contains(cycleKey))
            {
                Console.WriteLine("Circular master reference detected: " + cycleKey);
                reportedCycles.Add(cycleKey);
            }
            return;
        }

        // If this master has been processed before without finding a new cycle, skip it
        if (visitedGlobal.Contains(currentMaster.ID))
            return;

        // Mark the master as visited in the current path
        traversalStack.Add(currentMaster.ID);
        visitedGlobal.Add(currentMaster.ID);

        // Examine each shape inside the master
        foreach (Shape shape in currentMaster.Shapes)
        {
            // If the shape is based on another master, follow that reference
            if (shape.Master != null)
            {
                Master referencedMaster = shape.Master;
                if (masterLookup.TryGetValue(referencedMaster.ID, out Master targetMaster))
                {
                    DetectCircularReference(targetMaster, masterLookup, visitedGlobal, traversalStack, reportedCycles);
                }
            }
        }

        // Backtrack: remove the current master from the traversal stack
        traversalStack.RemoveAt(traversalStack.Count - 1);
    }
}
