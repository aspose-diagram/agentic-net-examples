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

            // Path to the source Visio file
            string sourcePath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(sourcePath);

            // Define the name of the shape to export
            string targetShapeName = "MyShape";

            // Find the shape with the specified universal name (NameU)
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU == targetShapeName)
                    {
                        targetShape = shape;
                        break;
                    }
                }
                if (targetShape != null)
                    break;
            }

            if (targetShape == null)
            {
                Console.WriteLine($"Shape \"{targetShapeName}\" not found.");
                return;
            }

            // Configure high‑resolution PNG export options
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
            pngOptions.Resolution = 300f; // DPI (higher value = higher resolution)

            // Output file path
            string outputPath = "exported_shape.png";

            // Export the selected shape to PNG
            targetShape.ToImage(outputPath, pngOptions);

            Console.WriteLine($"Shape \"{targetShapeName}\" exported to \"{outputPath}\" with {pngOptions.Resolution} DPI.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
