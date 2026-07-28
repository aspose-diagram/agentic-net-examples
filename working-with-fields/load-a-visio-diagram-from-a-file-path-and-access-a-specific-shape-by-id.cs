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
            string filePath = @"C:\Path\To\YourDiagram.vsdx";

            // Load the diagram using the constructor that accepts a file path
            Diagram diagram = new Diagram(filePath);

            // Specify the ID of the shape you want to access
            long shapeId = 123; // replace with the actual shape ID

            // Retrieve the shape from the active page by its ID
            Shape shape = diagram.ActivePage.Shapes.GetShape(shapeId);

            // Example usage: print shape details
            Console.WriteLine($"Shape ID: {shape.ID}, Name: {shape.Name}");

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
