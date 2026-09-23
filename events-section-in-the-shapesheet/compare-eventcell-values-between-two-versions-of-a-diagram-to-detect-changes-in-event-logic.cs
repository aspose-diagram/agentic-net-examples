using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two file paths: old version and new version
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: DiagramEventComparer <oldDiagramPath> <newDiagramPath>");
            return;
        }

        string oldPath = args[0];
        // Guard: ensure the old diagram file exists
        if (!File.Exists(oldPath)) { Console.Error.WriteLine($"File not found: {oldPath}"); return; }

        string newPath = args[1];
        // Guard: ensure the new diagram file exists
        if (!File.Exists(newPath)) { Console.Error.WriteLine($"File not found: {newPath}"); return; }

        Diagram oldDiagram;
        Diagram newDiagram;
        try
        {
            // Load the two diagrams (wrapped in try/catch for safety)
            oldDiagram = new Diagram(oldPath);
            newDiagram = new Diagram(newPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagrams: {ex.Message}");
            return;
        }

        // List of event cell names to compare
        List<string> eventNames = new List<string>
        {
            "EventXFMod",
            "EventDblClick",
            "EventDrop",
            "EventMultiDrop",
            "TheText",
            "TheData"
        };

        bool anyChanges = false;

        // Iterate pages by index (assuming same page order)
        int pageCount = Math.Min(oldDiagram.Pages.Count, newDiagram.Pages.Count);
        for (int p = 0; p < pageCount; p++)
        {
            Page oldPage = oldDiagram.Pages[p];
            Page newPage = newDiagram.Pages[p];

            // Iterate shapes in the old page
            foreach (Shape oldShape in oldPage.Shapes)
            {
                // Shape IDs are of type long; use long to avoid conversion errors
                long shapeId = oldShape.ID;
                Shape newShape = null;
                try
                {
                    // Retrieve the shape with the same ID from the new page
                    newShape = newPage.Shapes.GetShape(shapeId);
                }
                catch
                {
                    // Shape not found in new diagram; skip comparison
                    continue;
                }

                // Compare each event cell value between the two shapes
                foreach (string evName in eventNames)
                {
                    string oldFormula = GetEventFormula(oldShape, evName);
                    string newFormula = GetEventFormula(newShape, evName);

                    // Normalize nulls to empty strings for comparison
                    string oldVal = oldFormula ?? string.Empty;
                    string newVal = newFormula ?? string.Empty;

                    if (!oldVal.Equals(newVal, StringComparison.Ordinal))
                    {
                        anyChanges = true;
                        Console.WriteLine($"Page '{oldPage.Name}' Shape ID {shapeId} Event '{evName}' changed:");
                        Console.WriteLine($"    Old: \"{oldVal}\"");
                        Console.WriteLine($"    New: \"{newVal}\"");
                    }
                }
            }
        }

        if (!anyChanges)
        {
            Console.WriteLine("No event cell changes detected between the two diagrams.");
        }
    }

    // Retrieves the formula string of a specific event cell, or null if not present
    private static string GetEventFormula(Shape shape, string eventName)
    {
        if (shape?.Event == null)
            return null;

        switch (eventName)
        {
            case "EventXFMod":
                return shape.Event.EventXFMod?.Ufe?.F;
            case "EventDblClick":
                return shape.Event.EventDblClick?.Ufe?.F;
            case "EventDrop":
                return shape.Event.EventDrop?.Ufe?.F;
            case "EventMultiDrop":
                return shape.Event.EventMultiDrop?.Ufe?.F;
            case "TheText":
                return shape.Event.TheText?.Ufe?.F;
            case "TheData":
                return shape.Event.TheData?.Ufe?.F;
            default:
                return null;
        }
    }
}