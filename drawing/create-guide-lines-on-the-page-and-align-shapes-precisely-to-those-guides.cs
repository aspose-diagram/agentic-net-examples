using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define output file path
        string outputPath = "GuidesAndAlignedShape.vsdx";

        // Ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add a new page (the diagram starts with one page by default)
            // Use the first page for all operations
            Page page = diagram.Pages[0];

            // Define guide positions (in inches)
            double verticalGuideX = 2.0;   // X position for vertical guide
            double horizontalGuideY = 3.0; // Y position for horizontal guide

            // ------------------------------------------------------------
            // Create visual guide lines using line shapes (since Aspose.Diagram
            // does not expose a direct Guide collection API)
            // ------------------------------------------------------------

            // Vertical guide line (from top to bottom of the page)
            // Approximate page height; using a large value to ensure visibility
            double pageHeight = 10.0;
            long verticalLineId = page.DrawLine(verticalGuideX, 0.0, verticalGuideX, pageHeight);
            Shape verticalGuide = page.Shapes.GetShape(verticalLineId);
            // Set line color to light gray for visual distinction
            verticalGuide.Line.LineColor.Value = "#C0C0C0";

            // Horizontal guide line (from left to right of the page)
            double pageWidth = 10.0;
            long horizontalLineId = page.DrawLine(0.0, horizontalGuideY, pageWidth, horizontalGuideY);
            Shape horizontalGuide = page.Shapes.GetShape(horizontalLineId);
            horizontalGuide.Line.LineColor.Value = "#C0C0C0";

            // ------------------------------------------------------------
            // Add a sample rectangle shape that will be aligned to the guides
            // ------------------------------------------------------------
            double initialPinX = 1.0;
            double initialPinY = 1.0;
            long rectShapeId = diagram.AddShape(initialPinX, initialPinY, "Rectangle", 0);
            Shape rectShape = page.Shapes.GetShape(rectShapeId);

            // ------------------------------------------------------------
            // Align the rectangle shape precisely to the guide positions
            // ------------------------------------------------------------
            rectShape.XForm.PinX.Value = verticalGuideX;   // Align X to vertical guide
            rectShape.XForm.PinY.Value = horizontalGuideY; // Align Y to horizontal guide

            // Optionally, set a distinct fill color to highlight the aligned shape
            rectShape.Fill.FillForegnd.Value = "#FFCC00";

            // ------------------------------------------------------------
            // Save the diagram to a VSDX file
            // ------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}