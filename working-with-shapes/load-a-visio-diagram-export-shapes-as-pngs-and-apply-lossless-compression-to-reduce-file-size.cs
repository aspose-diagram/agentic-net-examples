using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure output directory exists
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory: {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate through each shape on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Build a unique file name for each shape PNG
                        string shapeFileName = Path.Combine(
                            outputFolder,
                            $"page{pageIndex + 1}_shape{shape.ID}.png");

                        // Configure PNG export options (PNG is inherently lossless)
                        ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
                        // No additional compression settings are required for lossless PNG

                        // Export the shape to PNG
                        shape.ToImage(shapeFileName, pngOptions);
                    }
                }
            }

            Console.WriteLine("Shape export completed successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}