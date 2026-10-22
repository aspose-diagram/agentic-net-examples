using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram.
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Aspose.Diagram.Page page = diagram.Pages[pageIndex];

                // Iterate through all shapes on the current page.
                for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                {
                    Shape shape = page.Shapes[shapeIndex];

                    // Check if the shape uses a gradient fill (pattern value 25) and has gradient stops.
                    if (shape.Fill != null &&
                        shape.Fill.FillPattern != null &&
                        shape.Fill.FillPattern.Value == 25 && // 25 = gradient fill pattern
                        shape.Fill.GradientFill != null &&
                        shape.Fill.GradientFill.GradientStops != null &&
                        shape.Fill.GradientFill.GradientStops.Count > 0)
                    {
                        // Update each gradient stop to the uniform gray color #808080.
                        foreach (GradientStop stop in shape.Fill.GradientFill.GradientStops)
                        {
                            // Preserve the existing position; only change the color.
                            stop.Color.Value = "#808080";
                        }
                    }
                }
            }

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}