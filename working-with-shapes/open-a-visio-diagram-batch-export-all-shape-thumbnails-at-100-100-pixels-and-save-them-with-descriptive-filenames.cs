using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Input Visio file path
            string inputPath = "input.vsdx";

            // Output directory for thumbnails
            string outputDir = "Thumbnails";

            // Ensure the output directory exists
            if (!Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Build a descriptive filename using page ID, shape ID and shape name (if available)
                    string shapeName = string.IsNullOrWhiteSpace(shape.NameU) ? "Shape" + shape.ID : shape.NameU;
                    // Replace characters that are invalid in file names
                    foreach (char c in Path.GetInvalidFileNameChars())
                    {
                        shapeName = shapeName.Replace(c, '_');
                    }

                    string fileName = Path.Combine(
                        outputDir,
                        $"Page_{page.ID}_Shape_{shape.ID}_{shapeName}.png");

                    // Configure image save options for a 100 × 100 pixel thumbnail
                    ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png);
                    options.PageSize = new PageSize(100f, 100f); // Width and height in pixels

                    // Export the shape thumbnail
                    shape.ToImage(fileName, options);
                }
            }

            // Optional: inform the user that processing is complete
            Console.WriteLine("Shape thumbnails have been exported successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
