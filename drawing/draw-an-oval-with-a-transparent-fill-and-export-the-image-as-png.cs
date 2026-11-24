using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputPngFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram contains at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Reference the first page.
                Page page = diagram.Pages[0];

                // Parameters for the oval (ellipse): center position (pinX, pinY) and size (width, height).
                double pinX = 2.0;   // X coordinate of the center.
                double pinY = 2.0;   // Y coordinate of the center.
                double width = 4.0;  // Horizontal diameter.
                double height = 2.0; // Vertical diameter.

                // Draw the ellipse on the page. The method returns the shape ID as a long.
                long shapeIdLong = page.DrawEllipse(pinX, pinY, width, height);

                // Retrieve the newly created shape using the integer ID.
                Shape ovalShape = page.Shapes.GetShape((int)shapeIdLong);

                // Set the fill to be fully transparent (100% transparent).
                // FillForegndTrans: 0.0 = opaque, 1.0 = fully transparent.
                ovalShape.Fill.FillForegndTrans.Value = 1.0;
                // Also make the background fill transparent (optional).
                ovalShape.Fill.FillBkgndTrans.Value = 1.0;

                // Prepare PNG export options.
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);

                // Save the diagram (including the transparent oval) as a PNG image.
                diagram.Save(outputPath, saveOptions);
            }

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}