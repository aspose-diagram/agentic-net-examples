using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define the output file path
        string outputPath = "output.vsdx";

        // Guard: ensure the output directory exists and is writable
        try
        {
            string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
            if (!Directory.Exists(outputDir))
            {
                Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to validate output path: {ex.Message}");
            return;
        }

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Access the first (and only) page
            Page page = diagram.Pages[0];

            // Retrieve page dimensions (in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Define rectangle size (in inches)
            double rectWidth = 1.0;
            double rectHeight = 0.5;

            // Calculate equal horizontal spacing:
            // spacing = (pageWidth - 3 * rectWidth) / 4
            double spacing = (pageWidth - 3 * rectWidth) / 4.0;
            if (spacing < 0)
            {
                Console.Error.WriteLine("Page width is too small to fit three rectangles with the desired size.");
                return;
            }

            // Y coordinate (center) for all rectangles
            double centerY = pageHeight / 2.0;

            // Add three identical rectangles and position them evenly
            for (int i = 0; i < 3; i++)
            {
                // Calculate center X for the current rectangle
                double centerX = spacing * (i + 1) + rectWidth * (i + 0.5);

                // Add shape using the built‑in "Rectangle" master; isCalculate set to false (plain bool)
                long shapeId = page.AddShape(centerX, centerY, "Rectangle", false);

                // Retrieve the shape object to set its dimensions
                Shape shape = page.Shapes.GetShape(shapeId);
                shape.XForm.Width.Value = rectWidth;
                shape.XForm.Height.Value = rectHeight;
            }

            // Save the diagram to VSDX format (requires a SaveFileFormat argument)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}