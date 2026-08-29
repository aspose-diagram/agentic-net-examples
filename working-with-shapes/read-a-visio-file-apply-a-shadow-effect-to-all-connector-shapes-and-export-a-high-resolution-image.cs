using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input Visio file path and output image file path.
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Guard: verify the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args.Length > 1 ? args[1] : "output.png";
        // Guard: ensure the directory for the output exists.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Identify connector shapes: they are 1‑D shapes (OneD == true).
                    if (shape.OneD)
                    {
                        // Enable a simple shadow for the connector.
                        shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;
                        // Set shadow color to black.
                        shape.Fill.ShdwForegnd.Value = "#000000";
                        // Set shadow transparency (30% transparent).
                        shape.Fill.ShdwForegndTrans.Value = 0.3;
                        // Set horizontal and vertical shadow offsets.
                        shape.Fill.ShapeShdwOffsetX.Value = 0.1;
                        shape.Fill.ShapeShdwOffsetY.Value = 0.1;
                    }
                }
            }

            // Configure high‑resolution image export options.
            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
            imgOptions.Resolution = 300f;               // 300 DPI for high quality.
            imgOptions.PageIndex = 0;                  // Start from the first page.
            imgOptions.PageCount = diagram.Pages.Count; // Export all pages (one file per page).

            // Save the diagram as a PNG image using the configured options.
            diagram.Save(outputPath, imgOptions);
        }
        catch (Exception ex)
        {
            // Write any exception details to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}