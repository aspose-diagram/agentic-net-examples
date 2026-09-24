using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Directory where individual shape images will be saved
        string outputDir = "ShapeImages";
        if (!Directory.Exists(outputDir))
            Directory.CreateDirectory(outputDir);

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure high‑quality JPEG export (anti‑aliasing not supported via property)
            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Jpeg);
            imgOptions.JpegQuality = 100;   // Maximum quality
            imgOptions.Resolution = 300f;   // 300 DPI for high resolution

            int exportedCount = 0;

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Build a unique file name for each shape
                    string fileName = $"Page{page.NameU}_Shape{shape.ID}_{exportedCount}.jpg";
                    string outputPath = Path.Combine(outputDir, fileName);

                    try
                    {
                        // Export the shape as a JPEG image
                        shape.ToImage(outputPath, imgOptions);
                        exportedCount++;
                    }
                    catch (Exception ex)
                    {
                        // Log any errors that occur while exporting a shape
                        Console.Error.WriteLine($"Error exporting shape ID {shape.ID}: {ex.Message}");
                    }
                }
            }

            Console.WriteLine($"Exported {exportedCount} shapes to folder: {outputDir}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during diagram loading or processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}