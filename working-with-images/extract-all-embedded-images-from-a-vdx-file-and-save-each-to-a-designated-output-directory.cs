using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input VDX file path and output directory.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: ExtractEmbeddedImages <input.vdx> <output_directory>");
            return;
        }

        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputDir = args[1];
        Directory.CreateDirectory(outputDir);

        int imageCount = 0;

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify embedded images (foreign shapes).
                    if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.Value != null && shape.ForeignData.Value.Length > 0)
                    {
                        byte[] imageData = shape.ForeignData.Value;

                        // Generate a unique file name using shape ID.
                        string fileName = $"Image_Shape{shape.ID}_{Guid.NewGuid():N}.png";
                        string outputPath = Path.Combine(outputDir, fileName);

                        // Save the raw image bytes to file.
                        File.WriteAllBytes(outputPath, imageData);
                        Console.WriteLine($"Extracted image to: {outputPath}");
                        imageCount++;
                    }
                }
            }

            Console.WriteLine($"Extraction complete. Total images extracted: {imageCount}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}