using System.IO;
using System;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Define the RGB color to match (foreground fill) and the new color to apply
            string targetColor = "#FF0000"; // Red in hex
            string newColor = "#00FF00";    // Green in hex

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Select shapes whose FillForegnd matches the target color and are not deleted
                var shapesToUpdate = page.Shapes
                    .Cast<Shape>()
                    .Where(s => s.Del == BOOL.False &&
                                string.Equals(s.Fill.FillForegnd.Value, targetColor, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                // Update the fill foreground color for each matched shape
                foreach (Shape shape in shapesToUpdate)
                {
                    shape.Fill.FillForegnd.Value = newColor;
                    Console.WriteLine($"Updated Shape ID {shape.ID} on page '{page.Name}' to color {newColor}");
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
