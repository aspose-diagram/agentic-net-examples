using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputDirectory]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        // Determine output directory (use current directory if not supplied)
        string outputDir = args.Length > 1 ? args[1] : Directory.GetCurrentDirectory();
        if (!Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: Unable to create output directory \"{outputDir}\": {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Build a descriptive filename:
                        // <sourceFileName>_Page<pageName>_Shape<shapeId>_<shapeName>.png
                        string baseFileName = Path.GetFileNameWithoutExtension(inputPath);
                        string pagePart = $"Page{SanitizeFileName(page.NameU)}";
                        string shapeIdPart = $"Shape{shape.ID}";
                        string shapeNamePart = string.IsNullOrWhiteSpace(shape.NameU) ? "" : $"_{SanitizeFileName(shape.NameU)}";
                        string fileName = $"{baseFileName}_{pagePart}_{shapeIdPart}{shapeNamePart}.png";

                        string outputPath = Path.Combine(outputDir, fileName);

                        // Configure image export options (100 × 100 pixels)
                        ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                        // Set the physical size of the exported image
                        imgOptions.PageSize = new PageSize(100f, 100f);
                        // Do not export hidden pages
                        imgOptions.ExportHiddenPage = false;

                        // Export the shape thumbnail
                        shape.ToImage(outputPath, imgOptions);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Helper method to replace invalid filename characters with an underscore
    private static string SanitizeFileName(string name)
    {
        if (string.IsNullOrEmpty(name))
            return "Unnamed";

        char[] invalidChars = Path.GetInvalidFileNameChars();
        foreach (char c in invalidChars)
        {
            name = name.Replace(c, '_');
        }
        return name;
    }
}