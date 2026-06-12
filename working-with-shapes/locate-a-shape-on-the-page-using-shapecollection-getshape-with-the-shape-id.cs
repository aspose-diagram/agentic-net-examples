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
            string filePath = "input.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(filePath);

            // Choose the page (e.g., the first page)
            Page page = diagram.Pages[0];

            // ID of the shape to locate
            long shapeId = 5; // replace with the actual shape ID

            // Locate the shape using ShapeCollection.GetShape(long ID)
            Shape shape = page.Shapes.GetShape(shapeId);

            // Output some information about the found shape
            Console.WriteLine($"Shape ID: {shape.ID}");
            Console.WriteLine($"Shape Name: {shape.Name}");
            Console.WriteLine($"Shape Type: {shape.Type}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
