using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // Specify the ID of the shape you want to export
            // Replace this with the actual shape ID in your diagram
            long shapeId = 1;

            // Retrieve the shape by its ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Export the selected shape to a high‑resolution PDF file
            // The ToPdf method uses the library's default high‑quality rendering
            shape.ToPdf("shape_output.pdf");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
