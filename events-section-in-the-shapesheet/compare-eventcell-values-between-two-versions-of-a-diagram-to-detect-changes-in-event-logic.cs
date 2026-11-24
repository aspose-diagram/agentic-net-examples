using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: old diagram path and new diagram path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <oldDiagramPath> <newDiagramPath>");
            return;
        }

        string oldPath = args[0];
        string newPath = args[1];

        // Guard against missing files
        if (!File.Exists(oldPath))
        {
            Console.Error.WriteLine($"File not found: {oldPath}");
            return;
        }
        if (!File.Exists(newPath))
        {
            Console.Error.WriteLine($"File not found: {newPath}");
            return;
        }

        try
        {
            // Load the old diagram inside a using block
            using (Diagram oldDiagram = new Diagram(oldPath))
            {
                // Load the new diagram inside a using block
                using (Diagram newDiagram = new Diagram(newPath))
                {
                    // Build a lookup of shapes from the new diagram by their ID
                    var newShapesById = new System.Collections.Generic.Dictionary<long, Shape>();
                    foreach (Page newPage in newDiagram.Pages)
                    {
                        foreach (Shape newShape in newPage.Shapes)
                        {
                            // Skip deleted shapes
                            if (newShape.Del == BOOL.True)
                                continue;

                            newShapesById[newShape.ID] = newShape;
                        }
                    }

                    // Iterate through pages and shapes of the old diagram
                    foreach (Page oldPage in oldDiagram.Pages)
                    {
                        foreach (Shape oldShape in oldPage.Shapes)
                        {
                            // Skip deleted shapes
                            if (oldShape.Del == BOOL.True)
                                continue;

                            long shapeId = oldShape.ID;

                            if (!newShapesById.TryGetValue(shapeId, out Shape newShape))
                            {
                                Console.WriteLine($"Shape ID {shapeId} exists in old diagram but not in new diagram.");
                                continue;
                            }

                            // Compare each relevant event cell
                            CompareEventCell("EventDrop", oldShape.Event.EventDrop?.Ufe?.F, newShape.Event.EventDrop?.Ufe?.F, shapeId);
                            CompareEventCell("EventDblClick", oldShape.Event.EventDblClick?.Ufe?.F, newShape.Event.EventDblClick?.Ufe?.F, shapeId);
                            CompareEventCell("EventXFMod", oldShape.Event.EventXFMod?.Ufe?.F, newShape.Event.EventXFMod?.Ufe?.F, shapeId);
                            CompareEventCell("EventMultiDrop", oldShape.Event.EventMultiDrop?.Ufe?.F, newShape.Event.EventMultiDrop?.Ufe?.F, shapeId);
                            CompareEventCell("TheText", oldShape.Event.TheText?.Ufe?.F, newShape.Event.TheText?.Ufe?.F, shapeId);
                            CompareEventCell("TheData", oldShape.Event.TheData?.Ufe?.F, newShape.Event.TheData?.Ufe?.F, shapeId);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagrams: {ex.Message}");
        }
    }

    // Helper method to compare two event formulas and report differences
    private static void CompareEventCell(string cellName, string oldFormula, string newFormula, long shapeId)
    {
        // Normalize nulls to empty strings for comparison
        string oldVal = oldFormula ?? string.Empty;
        string newVal = newFormula ?? string.Empty;

        if (!oldVal.Equals(newVal, StringComparison.Ordinal))
        {
            Console.WriteLine($"Shape ID {shapeId}: {cellName} changed.");
            Console.WriteLine($"    Old: \"{oldVal}\"");
            Console.WriteLine($"    New: \"{newVal}\"");
        }
    }
}