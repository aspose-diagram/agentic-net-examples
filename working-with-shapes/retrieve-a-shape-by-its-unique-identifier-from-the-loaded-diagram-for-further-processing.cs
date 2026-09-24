using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be loaded
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Unique identifier of the shape to retrieve (replace with actual ID)
            long shapeId = 12345;

            // Retrieve the first page (adjust if the shape resides on a different page)
            Page page = diagram.Pages[0];

            // Get the shape by its ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Example processing: output shape name and master name
            Console.WriteLine($"Shape ID: {shape.ID}");
            Console.WriteLine($"Shape NameU: {shape.NameU}");
            Console.WriteLine($"Master Name: {shape.Master?.Name ?? "No master"}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
