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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists.
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
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure image save options for high‑quality JPEG output.
                ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Jpeg);
                // Set a high resolution (e.g., 300 DPI) to improve rendering quality.
                imgOptions.Resolution = 300.0f;
                // High JPEG quality (0‑100).
                imgOptions.JpegQuality = 95;
                // No scaling; keep original size.
                imgOptions.Scale = 1.0f;

                int pageIndex = 0;
                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Build a unique file name for the shape image.
                        string fileName = $"Page{pageIndex + 1}_Shape{shape.ID}.jpg";
                        string outputPath = Path.Combine(outputFolder, fileName);

                        // Export the shape to a JPEG file using the configured options.
                        shape.ToImage(outputPath, imgOptions);
                    }
                    pageIndex++;
                }

                Console.WriteLine("Shape export completed successfully.");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}