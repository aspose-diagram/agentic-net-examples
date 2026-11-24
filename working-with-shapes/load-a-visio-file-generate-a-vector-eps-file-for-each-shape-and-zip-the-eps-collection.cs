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
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Define output locations.
        string outputDirectory = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "ShapeExports");
        string zipPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "Shapes.zip");

        try
        {
            // Ensure the export directory exists.
            if (!Directory.Exists(outputDirectory))
                Directory.CreateDirectory(outputDirectory);

            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page and each shape.
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];

                // Enumerate shapes on the current page.
                for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                {
                    Shape shape = page.Shapes[shapeIndex];

                    // Build a unique file name for the exported vector graphic.
                    string shapeFileName = $"Page{pageIndex + 1}_Shape{shape.ID}.svg";
                    string shapeFilePath = Path.Combine(outputDirectory, shapeFileName);

                    // Export the shape to a vector format.
                    // NOTE: Aspose.Diagram does not provide direct EPS export.
                    // SVG is used as the closest vector alternative.
                    ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Svg);
                    shape.ToImage(shapeFilePath, saveOptions);
                }
            }

            // Create a ZIP archive containing all exported files.
            if (File.Exists(zipPath))
                File.Delete(zipPath); // Remove existing ZIP if present.

            ZipFile.CreateFromDirectory(outputDirectory, zipPath, CompressionLevel.Optimal, false);

            Console.WriteLine($"Successfully exported shapes and created ZIP archive at \"{zipPath}\".");
        }
        catch (Exception ex)
        {
            // Capture any errors from Aspose.Diagram or I/O operations.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            // Optional cleanup: delete the temporary export folder.
            try
            {
                if (Directory.Exists(outputDirectory))
                    Directory.Delete(outputDirectory, true);
            }
            catch
            {
                // Suppress any cleanup errors.
            }
        }
    }
}