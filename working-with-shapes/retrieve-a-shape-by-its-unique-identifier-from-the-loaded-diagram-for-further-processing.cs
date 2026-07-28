using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio diagram file
            string inputPath = "input.vsdx";

            // Load the diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // Unique identifier of the shape to retrieve (example value)
            long shapeId = 5;

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Example processing: display basic shape information
            Console.WriteLine($"Shape ID: {shape.ID}");
            Console.WriteLine($"Shape Name: {shape.Name}");
            Console.WriteLine($"Master Name: {shape.Master?.Name ?? "No master"}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
