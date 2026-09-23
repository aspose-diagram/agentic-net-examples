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

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Access the first page and the first shape on that page
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes.GetShape(1); // shape ID 1

            // Ensure the shape has a gradient fill
            shape.Fill.FillPattern.Value = 25; // Gradient fill pattern
            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;

            // Set gradient direction to diagonal (top‑left to bottom‑right)
            // Direction value 2 corresponds to diagonal in Visio
            shape.Fill.GradientFill.GradientDir.Value = 2;

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
