using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Specify the unique shape ID you want to retrieve
            long targetShapeId = 12345; // example ID

            // Retrieve the shape from the first page (adjust page index if needed)
            Shape shape = diagram.Pages[0].Shapes.GetShape(targetShapeId);

            // Output the shape's Name property
            Console.WriteLine($"Shape Name: {shape.Name}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
