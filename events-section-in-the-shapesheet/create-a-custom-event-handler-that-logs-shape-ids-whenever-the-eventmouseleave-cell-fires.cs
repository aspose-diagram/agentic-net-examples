using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            Diagram diagram = new Diagram(inputPath);

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Use a supported event cell (e.g., EventDblClick) to demonstrate logging.
                    shape.Event.EventDblClick.Ufe.F = $"CALLTHIS(\"LogShapeId({shape.ID})\")";
                }
            }

            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // This method represents the macro that would be called from Visio.
    // In a real Visio environment, you would implement this as a VBA macro.
    // Here it simply writes the shape ID to the console for demonstration.
    public static void LogShapeId(long shapeId)
    {
        Console.WriteLine($"Event fired for shape ID: {shapeId}");
    }
}