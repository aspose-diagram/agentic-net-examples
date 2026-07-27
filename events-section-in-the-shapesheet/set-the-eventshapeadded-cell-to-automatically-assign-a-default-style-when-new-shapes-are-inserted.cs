using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for SaveFileFormat enum

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Get the first (default) page
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the page (returns the shape ID)
            long shapeId = page.AddShape(2.0, 2.0, "Rectangle");

            // Retrieve the shape object using its ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Set a supported event cell (EventXFMod) to call a macro that applies a default style.
            // EventShapeAdded does not exist; EventXFMod is the closest valid event cell.
            shape.Event.EventXFMod.Ufe.F = "CALLTHIS(\"ThisDocument.ApplyDefaultStyle\")";

            // Save the diagram as VSDX
            diagram.Save("Output.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}