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
            string filePath = "input.vsdx";

            // Load the diagram from the file
            Diagram diagram = new Diagram(filePath);

            // Retrieve the desired page (e.g., the first page)
            Page page = diagram.Pages[0];

            // Unique shape ID to retrieve (replace with the actual ID)
            long shapeId = 12345;

            // Get the shape by its ID from the specified page
            Shape shape = page.Shapes.GetShape(shapeId);

            // Example usage: display some basic information about the shape
            Console.WriteLine($"Shape ID: {shape.ID}, NameU: {shape.NameU}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
