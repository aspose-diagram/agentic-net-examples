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
            string visioFile = "sample.vsdx";

            // ID of the shape you want to retrieve
            long shapeId = 12345;

            // Load the diagram using the constructor that accepts a file name
            Diagram diagram = new Diagram(visioFile);

            // Access the first page (index 0) and get the shape by its ID
            Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

            // Example usage: output some properties of the retrieved shape
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
