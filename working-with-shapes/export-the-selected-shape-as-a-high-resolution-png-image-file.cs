using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to load
            string sourcePath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(sourcePath);

            // Get the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // Identify the shape to export.
            // Replace the ID with the actual shape ID you want to export.
            long shapeId = 1;
            Shape shape = page.Shapes.GetShape(shapeId);

            if (shape == null)
            {
                Console.WriteLine($"Shape with ID {shapeId} not found.");
                return;
            }

            // Configure high‑resolution PNG export options
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
            pngOptions.Resolution = 300f; // 300 DPI for high resolution

            // Output file path
            string outputPath = "exported_shape.png";

            // Export the selected shape to PNG
            shape.ToImage(outputPath, pngOptions);

            Console.WriteLine($"Shape exported successfully to '{outputPath}' with resolution {pngOptions.Resolution} DPI.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
