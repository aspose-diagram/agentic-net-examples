using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file
            string filePath = "example.vsdx";

            // ID of the shape you want to access
            long shapeId = 5;

            // Load the diagram from the specified file
            Diagram diagram = new Diagram(filePath);

            // Get the first page (or use diagram.ActivePage)
            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID
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
