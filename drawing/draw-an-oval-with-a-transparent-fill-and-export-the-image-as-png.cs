using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputPngFile]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length >= 2 ? args[1] : "output.png";

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

            // Ensure there is at least one page to draw on.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Draw an oval (ellipse) on the first page.
            // Parameters: pinX, pinY, width, height.
            // Example values place the oval near the origin.
            long shapeId = diagram.Pages[0].DrawEllipse(1.0, 2.0, 4.0, 2.0);

            // Retrieve the newly created shape.
            Shape ovalShape = diagram.Pages[0].Shapes.GetShape(shapeId);

            // Set the fill to be fully transparent.
            // FillForegndTrans.Value = 1.0 means 100% transparent.
            ovalShape.Fill.FillForegndTrans.Value = 1.0;

            // Optionally, ensure a solid fill pattern is defined (required by Visio).
            ovalShape.Fill.FillPattern.Value = 1; // Solid fill pattern.

            // Prepare PNG export options.
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);

            // Save the diagram (including the oval) as a PNG image.
            diagram.Save(outputPath, saveOptions);

            Console.WriteLine($"Diagram saved as PNG to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}