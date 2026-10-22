using System;
using System.IO;
using System.IO.Compression;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputZipPath>");
            return;
        }

        string inputPath = args[0];
        string outputZipPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a temporary folder for individual EPS (SVG used as substitute) files.
        string tempFolder = Path.Combine(Path.GetTempPath(), "ShapeEps_" + Guid.NewGuid().ToString("N"));
        try
        {
            Directory.CreateDirectory(tempFolder);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create temporary folder: {ex.Message}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (Diagram does not implement IDisposable, but this follows the required pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over pages using an index because Page does not have an Index property.
                for (int pageIdx = 0; pageIdx < diagram.Pages.Count; pageIdx++)
                {
                    Page page = diagram.Pages[pageIdx];

                    // Iterate over each shape on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Build a unique file name for the exported shape.
                        // EPS export is not directly supported by Aspose.Diagram; SVG is used as the closest vector alternative.
                        string shapeFileName = $"page{pageIdx + 1}_shape{shape.ID}.svg";
                        string shapeFilePath = Path.Combine(tempFolder, shapeFileName);

                        // Export the shape to SVG (vector format) using ImageSaveOptions.
                        ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Svg);
                        shape.ToImage(shapeFilePath, saveOptions);
                    }
                }
            }

            // If the output ZIP already exists, delete it to avoid exceptions.
            if (File.Exists(outputZipPath))
            {
                try
                {
                    File.Delete(outputZipPath);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Unable to delete existing ZIP file: {ex.Message}");
                    return;
                }
            }

            // Create a ZIP archive containing all exported shape files.
            ZipFile.CreateFromDirectory(tempFolder, outputZipPath);
            Console.WriteLine($"Successfully created ZIP archive: {outputZipPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during processing: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary folder.
            try
            {
                if (Directory.Exists(tempFolder))
                {
                    Directory.Delete(tempFolder, true);
                }
            }
            catch (Exception cleanupEx)
            {
                Console.Error.WriteLine($"Failed to clean up temporary files: {cleanupEx.Message}");
            }
        }
    }
}