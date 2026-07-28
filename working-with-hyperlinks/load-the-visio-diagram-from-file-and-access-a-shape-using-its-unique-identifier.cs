using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file (e.g., VSDX format)
            var diagram = new Diagram("sample.vsdx");

            // Unique identifier of the shape you want to access
            long shapeId = 12345; // replace with the actual shape ID

            // Access the shape from the first page (index 0) using its ID
            Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

            // Example usage: output some shape properties
            Console.WriteLine($"Shape ID: {shape.ID}");
            Console.WriteLine($"Shape Name: {shape.Name}");
            Console.WriteLine($"Shape Text: {shape.Text?.Value}");

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
