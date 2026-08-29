using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Unique ID of the shape to retrieve
            long shapeId = 12345; // replace with the actual ID

            // Retrieve the shape by its ID from the first page (adjust page index if needed)
            Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

            // Output the shape's Name property
            Console.WriteLine($"Shape Name: {shape.Name}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
