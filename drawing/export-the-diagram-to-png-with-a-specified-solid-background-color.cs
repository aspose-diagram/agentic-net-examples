using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output PNG path, background color in hex (e.g., "#FF0000")
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.png> <backgroundColorHex>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string bgColorHex = args[2];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Work with the first page (index 0)
            Page page = diagram.Pages[0];

            // Retrieve page dimensions (in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Add a rectangle shape that covers the entire page.
            // Using the built‑in "Rectangle" master.
            long shapeId = page.AddShape(0, 0, pageWidth, pageHeight, "Rectangle");
            Shape backgroundShape = page.Shapes.GetShape(shapeId);

            // Set the shape's fill pattern to solid (value 1)
            backgroundShape.Fill.FillPattern.Value = 1;

            // Set the solid fill color to the specified background color
            backgroundShape.Fill.FillForegnd.Value = bgColorHex;

            // Send the shape to back so it appears behind all other content
            backgroundShape.SendToBack();

            // Configure PNG export options
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
            // Optional: set resolution, page range, etc., if needed
            // pngOptions.Resolution = 300f;
            // pngOptions.PageIndex = 0;
            // pngOptions.PageCount = 1;

            // Export the diagram to PNG using the configured options
            diagram.Save(outputPath, pngOptions);

            // Clean up resources
            diagram.Dispose();

            Console.WriteLine($"Diagram exported successfully to '{outputPath}' with background color {bgColorHex}.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}