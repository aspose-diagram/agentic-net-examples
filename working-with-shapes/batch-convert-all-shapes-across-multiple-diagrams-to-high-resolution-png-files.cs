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

            // Folder containing the source Visio files
            string inputFolder = @"InputDiagrams";
            // Folder where the PNG images will be saved
            string outputRoot = @"OutputImages";

            // Get all Visio files (adjust extensions as needed)
            string[] diagramFiles = Directory.GetFiles(inputFolder, "*.vsdx");

            foreach (string diagramPath in diagramFiles)
            {
                // Load the diagram using the provided constructor
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // Iterate through each page in the diagram
                    for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                    {
                        var page = diagram.Pages[pageIndex];

                        // Iterate through each shape on the current page
                        for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                        {
                            var shape = page.Shapes[shapeIndex];

                            // Build output directory and file name
                            string diagramName = Path.GetFileNameWithoutExtension(diagramPath);
                            string pageFolder = Path.Combine(outputRoot, diagramName, $"Page{pageIndex + 1}");
                            Directory.CreateDirectory(pageFolder);
                            string outputFile = Path.Combine(pageFolder, $"Shape{shape.ID}.png");

                            // Configure high‑resolution PNG options
                            ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Png);
                            // Set desired DPI (property name may vary in actual library)
                            options.Resolution = 300; // 300 DPI for high resolution

                            // Export the shape to PNG using the provided ToImage method
                            shape.ToImage(outputFile, options);
                        }
                    }
                }
            }

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
