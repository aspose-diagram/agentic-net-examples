using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string diagramPath = "input.vsdx"; // replace with your file path
            Diagram diagram = new Diagram(diagramPath);

            // Specify the page index (0‑based) from which to retrieve the shape
            int pageIndex = 0; // change as needed
            Page page = diagram.Pages[pageIndex];

            // Unique identifier of the shape to retrieve
            long shapeId = 12345; // replace with the actual shape ID

            // Retrieve the shape by its ID from the specified page
            Shape shape = page.Shapes.GetShape(shapeId);

            // Example: output some basic information about the shape
            Console.WriteLine($"Shape ID: {shape.ID}");
            Console.WriteLine($"Shape Name: {shape.Name}");
            Console.WriteLine($"Shape Master: {shape.Master?.Name ?? "None"}");

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
