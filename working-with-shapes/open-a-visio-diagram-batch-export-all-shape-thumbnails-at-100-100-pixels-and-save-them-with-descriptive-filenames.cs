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
            string inputPath = @"C:\Diagrams\sample.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Prepare image save options with a fixed 100x100 pixel size
            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
            // Set the physical size of the exported image (width, height) in pixels
            imgOptions.PageSize = new PageSize(100f, 100f);
            // Do not export hidden pages (optional)
            imgOptions.ExportHiddenPage = false;

            // Base output folder
            string outputFolder = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "ShapeThumbnails");
            Directory.CreateDirectory(outputFolder);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Build a descriptive filename
                    string shapeName = !string.IsNullOrWhiteSpace(shape.NameU) ? shape.NameU : shape.Name;
                    string safeShapeName = SanitizeFileName(shapeName);
                    string fileName = $"{Path.GetFileNameWithoutExtension(inputPath)}_Page{page.ID}_Shape{shape.ID}_{safeShapeName}.png";
                    string outputPath = Path.Combine(outputFolder, fileName);

                    // Export the shape thumbnail
                    shape.ToImage(outputPath, imgOptions);
                    Console.WriteLine($"Exported shape ID {shape.ID} to {outputPath}");
                }
            }

            Console.WriteLine("All shape thumbnails have been exported.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Removes characters that are invalid in file names
    private static string SanitizeFileName(string name)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(c, '_');
        }
        return name;
    }
}
