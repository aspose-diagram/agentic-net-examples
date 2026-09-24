using System.IO;
using System;
using System.Linq;
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

            // Define the target fill foreground color (hex RGB) and the new color to apply
            string targetColor = "#FF0000"; // Red
            string newColor = "#00FF00";    // Green

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Select shapes whose FillForegnd matches the target color using LINQ
                var matchingShapes = page.Shapes
                    .Cast<Shape>()
                    .Where(s => s.Fill.FillForegnd.Value.Equals(targetColor, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                // Update the fill foreground color for each matching shape
                foreach (Shape shape in matchingShapes)
                {
                    shape.Fill.FillForegnd.Value = newColor;
                }
            }

            // Save the updated diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
