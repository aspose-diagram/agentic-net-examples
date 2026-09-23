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
            string filePath = @"C:\Diagrams\sample.vsdx";

            // Load the diagram from the file
            Diagram diagram = new Diagram(filePath);

            // Access the first page (index 0)
            Page page = diagram.Pages[0];

            // ID of the shape you want to retrieve
            long shapeId = 12345; // replace with the actual shape ID

            // Retrieve the shape by its ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Output some basic information about the shape
            Console.WriteLine($"Shape ID: {shape.ID}");
            Console.WriteLine($"Shape NameU: {shape.NameU}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
