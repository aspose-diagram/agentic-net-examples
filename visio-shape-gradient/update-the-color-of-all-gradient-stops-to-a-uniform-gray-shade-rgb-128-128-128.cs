using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputVisioFile]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];

                // Iterate through all shapes on the current page
                for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                {
                    Shape shape = page.Shapes[shapeIndex];

                    // Process only shapes that use a gradient fill (FillPattern value 25)
                    if (shape.Fill != null && shape.Fill.FillPattern != null && shape.Fill.FillPattern.Value == 25)
                    {
                        // Ensure the gradient is enabled
                        if (shape.Fill.GradientFill != null && shape.Fill.GradientFill.GradientEnabled != null)
                        {
                            shape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;
                        }

                        // Update each gradient stop to the uniform gray color (RGB 128,128,128)
                        if (shape.Fill.GradientFill != null && shape.Fill.GradientFill.GradientStops != null)
                        {
                            foreach (GradientStop stop in shape.Fill.GradientFill.GradientStops)
                            {
                                // Set the stop color to gray using a hex string
                                stop.Color.Value = "#808080";
                            }
                        }
                    }
                }
            }

            // Save the modified diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
