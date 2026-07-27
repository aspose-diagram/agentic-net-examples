using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Verify that a diagram file path was provided.
        if (args.Length == 0)
        {
            Console.WriteLine("Usage: EventMouseDownLogger <diagram-file-path>");
            return;
        }

        string diagramPath = args[0];
        // Guard to ensure the diagram file exists before attempting to load it.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // Load the Visio diagram inside a try/catch to handle any loading errors.
        Diagram diagram;
        try
        {
            diagram = new Diagram(diagramPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Iterate through all pages and shapes to log the EventDrop formula (used as a stand‑in for EventMouseDown).
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                // Ensure the Event section exists and the EventDrop cell is present.
                if (shape.Event != null && shape.Event.EventDrop != null)
                {
                    // Retrieve the formula from the EventDrop cell; use empty string if null.
                    string formula = shape.Event.EventDrop.Ufe.F ?? string.Empty;
                    Console.WriteLine($"Page ID: {page.ID}, Shape ID: {shape.ID}, EventDrop Formula: {formula}");
                }
            }
        }
    }
}