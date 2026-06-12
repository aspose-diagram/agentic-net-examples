using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file (replace with your actual file path)
            var diagram = new Diagram("sample.vsdx");

            // Access the first page (or any specific page you need)
            var page = diagram.Pages[0];

            // Specify the shape ID you want to retrieve
            long shapeId = 5; // example ID

            // Get the shape by its ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Example usage: output shape details
            Console.WriteLine($"Shape ID: {shape.ID}, Name: {shape.Name}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
