using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file (uses the Diagram(string) constructor)
            var diagram = new Diagram("sample.vsdx");

            // Identifier of the shape we want to locate
            long shapeId = 12345; // replace with the actual shape ID

            // Access the first page (or any specific page as needed)
            var page = diagram.Pages[0];

            // Locate the shape by its ID using the ShapeCollection.GetShape method
            Shape shape = page.Shapes.GetShape(shapeId);

            // Example processing: display some basic information about the shape
            Console.WriteLine($"Found shape with ID: {shape.ID}");
            Console.WriteLine($"Shape Name: {shape.Name}");
            Console.WriteLine($"Shape Type: {shape.Type}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
