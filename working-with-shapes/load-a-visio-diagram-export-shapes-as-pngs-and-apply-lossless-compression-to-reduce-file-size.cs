using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        // Output directory for exported PNGs (second argument or default)
        string outputDir = args.Length > 1 ? args[1] : "ExportedShapes";

        // Ensure the output directory exists
        try
        {
            Directory.CreateDirectory(outputDir);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating output directory '{outputDir}': {ex.Message}");
            return;
        }

        // Load the Visio diagram
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Prepare PNG save options (lossless by default)
        ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
        // PNG export is lossless; no additional compression settings are required.

        int totalExported = 0;

        // Iterate through each page
        for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
        {
            Page page = diagram.Pages[pageIndex];

            // Iterate through each shape on the page
            foreach (Shape shape in page.Shapes)
            {
                // Build a unique file name for the shape PNG
                string shapeFileName = $"{Path.GetFileNameWithoutExtension(inputPath)}_Page{pageIndex + 1}_Shape{shape.ID}.png";
                string shapeFilePath = Path.Combine(outputDir, shapeFileName);

                try
                {
                    // Export the shape to PNG using the prepared options
                    shape.ToImage(shapeFilePath, pngOptions);
                    totalExported++;
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error exporting shape ID {shape.ID} on page {pageIndex + 1}: {ex.Message}");
                    // Continue with next shape
                }
            }
        }

        Console.WriteLine($"Export completed. Total shapes exported: {totalExported}");
    }
}