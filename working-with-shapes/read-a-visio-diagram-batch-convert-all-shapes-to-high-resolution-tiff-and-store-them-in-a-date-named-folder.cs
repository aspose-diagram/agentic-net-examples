using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a date‑named output folder (e.g., 20231119)
        string outputFolder = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? Directory.GetCurrentDirectory(),
            DateTime.Now.ToString("yyyyMMdd"));

        try
        {
            Directory.CreateDirectory(outputFolder);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create output directory '{outputFolder}': {ex.Message}");
            return;
        }

        // Load the Visio diagram inside a using block as required
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare high‑resolution TIFF save options
                ImageSaveOptions tiffOptions = new ImageSaveOptions(SaveFileFormat.Tiff)
                {
                    // Set a high DPI for better quality (Resolution is DPI)
                    Resolution = 300f,
                    // Use LZW compression for TIFF
                    TiffCompression = TiffCompression.Lzw
                };

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Build a safe file name using shape ID and name
                        string safeName = MakeSafeFileName(shape.NameU);
                        string fileName = $"Page{page.ID}_Shape{shape.ID}_{safeName}.tiff";
                        string outputPath = Path.Combine(outputFolder, fileName);

                        try
                        {
                            // Export the shape to a high‑resolution TIFF image
                            shape.ToImage(outputPath, tiffOptions);
                            Console.WriteLine($"Exported shape ID {shape.ID} to {outputPath}");
                        }
                        catch (Exception exShape)
                        {
                            Console.Error.WriteLine($"Failed to export shape ID {shape.ID}: {exShape.Message}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Helper method to remove invalid filename characters and limit length
    private static string MakeSafeFileName(string name)
    {
        if (string.IsNullOrEmpty(name))
            return "Unnamed";

        foreach (char c in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(c, '_');
        }

        // Trim length if necessary
        return name.Length > 50 ? name.Substring(0, 50) : name;
    }
}