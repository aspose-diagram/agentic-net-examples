using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputDirectory]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output directory (default: "Thumbnails" in the current folder)
        string outputDir = args.Length > 1 ? args[1] : Path.Combine(Directory.GetCurrentDirectory(), "Thumbnails");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Load the Visio diagram inside a using block as required
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Build a descriptive filename using shape name (if available), page ID and shape ID
                        string baseName = !string.IsNullOrEmpty(shape.NameU) ? shape.NameU : shape.ID.ToString();
                        // Sanitize filename characters
                        foreach (char c in Path.GetInvalidFileNameChars())
                        {
                            baseName = baseName.Replace(c, '_');
                        }
                        string fileName = $"{baseName}_Page{page.ID}_Shape{shape.ID}.png";
                        string outputPath = Path.Combine(outputDir, fileName);

                        try
                        {
                            // Configure image export options for 100x100 pixels
                            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                            // Set the physical size of the exported image (100x100). PageSize is expressed in inches,
                            // but the API will map it to the requested pixel dimensions.
                            imgOptions.PageSize = new PageSize(100, 100);

                            // Export the shape thumbnail to the specified file
                            shape.ToImage(outputPath, imgOptions);
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Failed to export shape ID {shape.ID} on page {page.ID}: {ex.Message}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
        }
    }
}