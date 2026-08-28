using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    // List of event cell names to compare
    private static readonly string[] EventNames = new[]
    {
        "EventXFMod",
        "EventDblClick",
        "EventDrop",
        "EventMultiDrop",
        "TheText",
        "TheData"
    };

    static void Main()
    {
        try
        {

            // Paths to the two diagram versions
            string oldDiagramPath = "oldDiagram.vsdx";
            string newDiagramPath = "newDiagram.vsdx";

            // Load diagrams
            Diagram oldDiagram = new Diagram(oldDiagramPath);
            Diagram newDiagram = new Diagram(newDiagramPath);

            // Iterate through pages by index (assuming same page order)
            int pageCount = oldDiagram.Pages.Count;
            for (int i = 0; i < pageCount; i++)
            {
                Page oldPage = oldDiagram.Pages[i];
                Page newPage = newDiagram.Pages[i];

                // Iterate through shapes on the old page
                foreach (Shape oldShape in oldPage.Shapes)
                {
                    // Try to find the corresponding shape in the new diagram by ID
                    Shape newShape = newPage.Shapes.GetShape(oldShape.ID);
                    if (newShape == null)
                    {
                        Console.WriteLine($"Shape ID {oldShape.ID} not found in new diagram (page {newPage.Name}).");
                        continue;
                    }

                    // Compare each event cell
                    foreach (string eventName in EventNames)
                    {
                        string oldFormula = GetEventFormula(oldShape, eventName);
                        string newFormula = GetEventFormula(newShape, eventName);

                        // If both are empty, no need to report
                        if (string.IsNullOrEmpty(oldFormula) && string.IsNullOrEmpty(newFormula))
                            continue;

                        // Detect change
                        if (!oldFormula.Equals(newFormula, StringComparison.Ordinal))
                        {
                            Console.WriteLine($"Change detected in shape ID {oldShape.ID} (Name: {oldShape.Name}) on page '{oldPage.Name}':");
                            Console.WriteLine($"  Event: {eventName}");
                            Console.WriteLine($"  Old formula: \"{oldFormula}\"");
                            Console.WriteLine($"  New formula: \"{newFormula}\"");
                        }
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Retrieves the formula string for a given event cell; returns empty string if not set
    private static string GetEventFormula(Shape shape, string eventName)
    {
        if (shape?.Event == null)
            return string.Empty;

        return eventName switch
        {
            "EventXFMod" => shape.Event.EventXFMod?.Ufe?.F ?? string.Empty,
            "EventDblClick" => shape.Event.EventDblClick?.Ufe?.F ?? string.Empty,
            "EventDrop" => shape.Event.EventDrop?.Ufe?.F ?? string.Empty,
            "EventMultiDrop" => shape.Event.EventMultiDrop?.Ufe?.F ?? string.Empty,
            "TheText" => shape.Event.TheText?.Ufe?.F ?? string.Empty,
            "TheData" => shape.Event.TheData?.Ufe?.F ?? string.Empty,
            _ => string.Empty,
        };
    }
}
