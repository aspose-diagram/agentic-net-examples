using System.IO;
using System;
using Aspose.Diagram;

class LocateShapeExample
{
    static void Main()
    {
        try
        {

            // Assume a diagram is already loaded elsewhere and passed to this method.
            // For demonstration, we reference an existing Diagram instance named 'diagram'.
            Diagram diagram = new Diagram("input.vsdx"); // Load an existing Visio file

            // Select the page that contains the shape (e.g., the first page)
            Page page = diagram.Pages[0];

            // Specify the shape ID you want to locate
            long shapeId = 5; // replace with the actual ID

            // Locate the shape using ShapeCollection.GetShape(long ID)
            Shape shape = page.Shapes.GetShape(shapeId);

            // Example usage: output some properties of the found shape
            Console.WriteLine($"Found shape with ID: {shape.ID}");
            Console.WriteLine($"Shape Name: {shape.Name}");
            Console.WriteLine($"Shape Text: {shape.Text}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
