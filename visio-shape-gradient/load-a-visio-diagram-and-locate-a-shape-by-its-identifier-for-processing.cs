using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be loaded
            string filePath = "sample.vsdx";

            // Load the diagram from the file
            Diagram diagram = new Diagram(filePath);

            // Access the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // Identifier of the shape to locate (replace with actual ID)
            long shapeId = 5;

            // Retrieve the shape by its ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Example processing: display shape details
            Console.WriteLine($"Shape ID: {shape.ID}, Name: {shape.Name}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
