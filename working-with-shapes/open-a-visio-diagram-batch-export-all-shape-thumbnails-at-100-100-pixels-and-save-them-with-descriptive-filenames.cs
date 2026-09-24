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

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Directory where thumbnails will be saved
            string outputDir = "Thumbnails";
            Directory.CreateDirectory(outputDir);

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Prepare a descriptive file name
                    string pageName = string.IsNullOrWhiteSpace(page.NameU) ? "Page" + page.ID : page.NameU;
                    string shapeName = string.IsNullOrWhiteSpace(shape.NameU) ? "Shape" + shape.ID : shape.NameU;
                    string rawFileName = $"{pageName}_{shape.ID}_{shapeName}.png";

                    // Remove characters that are invalid in file names
                    foreach (char c in Path.GetInvalidFileNameChars())
                    {
                        rawFileName = rawFileName.Replace(c, '_');
                    }

                    string outputPath = Path.Combine(outputDir, rawFileName);

                    // Configure image export options for a 100 × 100 pixel thumbnail
                    ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png);
                    options.PageSize = new PageSize(100f, 100f);
                    options.ExportHiddenPage = false;

                    // Export the shape as an image
                    shape.ToImage(outputPath, options);
                }
            }

            Console.WriteLine("Thumbnail export completed.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
