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

            // Load the diagram
            Diagram diagram = new Diagram(filePath);

            // ID of the shape whose dimensions are needed
            long shapeId = 5; // replace with the actual shape ID

            // Access the first page (adjust if the shape is on a different page)
            Page page = diagram.Pages[0];

            // Retrieve the shape by its ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Get width and height from the shape's XForm
            double width = shape.XForm.Width.Value;
            double height = shape.XForm.Height.Value;

            // Output the dimensions
            Console.WriteLine($"Shape ID: {shapeId}");
            Console.WriteLine($"Width: {width} inches");
            Console.WriteLine($"Height: {height} inches");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
