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

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Guard: ensure the output folder exists (create if necessary).
        try
        {
            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create output folder '{outputFolder}': {ex.Message}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure image export options with a custom DPI of 600.
                ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Jpeg)
                {
                    // Set the resolution (DPI) for the exported image.
                    Resolution = 600f,
                    // Set JPEG quality (0‑100). 100 = best quality.
                    JpegQuality = 100,
                    // Do not export hidden shapes.
                    ExportHiddenPage = false
                };

                // Iterate through each page.
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate through each shape on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Build a unique file name for the shape image.
                        string shapeFileName = $"{Path.GetFileNameWithoutExtension(inputPath)}_Page{pageIndex + 1}_Shape{shape.ID}.jpg";
                        string shapeFilePath = Path.Combine(outputFolder, shapeFileName);

                        try
                        {
                            // Export the current shape to JPEG using the configured options.
                            shape.ToImage(shapeFilePath, imgOptions);
                            Console.WriteLine($"Exported shape ID {shape.ID} to '{shapeFilePath}'.");
                        }
                        catch (Exception shapeEx)
                        {
                            Console.Error.WriteLine($"Failed to export shape ID {shape.ID} on page {pageIndex + 1}: {shapeEx.Message}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Catch any errors that occur during diagram loading or processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}