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

            // Choose the page that contains the shape (here we use the first page)
            var page = diagram.Pages[0];

            // Specify the ID of the shape you want to retrieve
            long shapeId = 5; // example ID; replace with the actual ID you need

            // Retrieve the shape by its ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Example usage: output some properties of the retrieved shape
            Console.WriteLine($"Shape ID: {shape.ID}");
            Console.WriteLine($"Shape Name: {shape.Name}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
