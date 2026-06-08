using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to load
            string filePath = "sample.vsdx";

            // Load the diagram using the built‑in constructor (load rule)
            Diagram diagram = new Diagram(filePath);

            // ID of the shape you want to access
            long shapeId = 5; // replace with the actual shape ID

            // Get the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID (shape collection rule)
            Shape shape = page.Shapes.GetShape(shapeId);

            // Example usage: print shape details
            Console.WriteLine($"Shape ID: {shape.ID}, Name: {shape.Name}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
