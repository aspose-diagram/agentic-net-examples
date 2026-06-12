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
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // Identify the target shape (replace with the actual shape ID)
            long shapeId = 1; // example ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Disable line inheritance by explicitly setting line properties
            // Set a new line weight (thickness) in inches
            shape.Line.LineWeight.Value = 0.05; // 0.05 inches

            // Optionally set a line color to ensure the line properties are no longer inherited
            shape.Line.LineColor.Value = "#FF0000";

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
