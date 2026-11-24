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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
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

        // Load the Visio diagram inside a using block as required.
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram.
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate through each shape on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Prepare JPEG save options with highest quality.
                        ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Jpeg);
                        saveOptions.JpegQuality = 100; // Highest JPEG quality.

                        // NOTE: Aspose.Diagram.ImageSaveOptions does not expose an AntiAliasing property.
                        // Anti‑aliasing cannot be set directly via the API; the rendering engine applies its
                        // default smoothing. If explicit anti‑aliasing control is required, consider using
                        // a different rendering library or post‑process the image.

                        // Build a unique file name for the exported shape.
                        string shapeFileName = $"{Path.GetFileNameWithoutExtension(inputPath)}_page{pageIndex + 1}_shape{shape.ID}.jpg";
                        string outputPath = Path.Combine(outputFolder, shapeFileName);

                        try
                        {
                            // Export the current shape to a JPEG file.
                            shape.ToImage(outputPath, saveOptions);
                            Console.WriteLine($"Exported shape ID {shape.ID} to {outputPath}");
                        }
                        catch (Exception shapeEx)
                        {
                            // Log shape‑specific errors but continue processing other shapes.
                            Console.Error.WriteLine($"Failed to export shape ID {shape.ID}: {shapeEx.Message}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during diagram loading or processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}