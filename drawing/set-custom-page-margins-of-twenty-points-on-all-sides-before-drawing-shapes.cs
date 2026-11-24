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

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Create a new blank diagram inside a using block as required
            using (Diagram diagram = new Diagram())
            {
                // Access the first (default) page
                Page page = diagram.Pages[0];

                // Convert 20 points to inches (1 point = 1/72 inch)
                double marginInches = 20.0 / 72.0;

                // Set custom margins on all sides using PrintProps
                page.PageSheet.PrintProps.PageLeftMargin.Value = marginInches;
                page.PageSheet.PrintProps.PageRightMargin.Value = marginInches;
                page.PageSheet.PrintProps.PageTopMargin.Value = marginInches;
                page.PageSheet.PrintProps.PageBottomMargin.Value = marginInches;

                // Draw a simple ellipse shape after setting margins
                // Parameters: pinX, pinY, width, height (all in inches)
                double pinX = 2.0;   // X coordinate of the ellipse center
                double pinY = 2.0;   // Y coordinate of the ellipse center
                double width = 1.0;  // Width of the ellipse
                double height = 0.5; // Height of the ellipse
                long shapeId = page.DrawEllipse(pinX, pinY, width, height);

                // Retrieve the shape to modify its properties if needed
                Shape shape = page.Shapes.GetShape(shapeId);
                // Example: set a fill color for the ellipse
                shape.Fill.FillForegnd.Value = "#FFCC00"; // Orange fill

                // Save the diagram to VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}