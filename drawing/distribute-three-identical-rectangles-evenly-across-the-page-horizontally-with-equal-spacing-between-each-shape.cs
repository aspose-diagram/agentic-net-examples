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

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Access the first page (index 0)
            Page page = diagram.Pages[0];

            // Retrieve page dimensions (in inches)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Define rectangle size (in inches)
            double rectWidth = 1.5;
            double rectHeight = 1.0;

            // Number of rectangles to distribute
            int shapeCount = 3;

            // Calculate horizontal spacing so that rectangles are evenly distributed:
            // spacing = (pageWidth - totalRectWidth) / (shapeCount + 1)
            double totalRectWidth = rectWidth * shapeCount;
            double spacing = (pageWidth - totalRectWidth) / (shapeCount + 1);

            // Vertical position (center of the page)
            double centerY = pageHeight / 2.0;

            // Master name for a rectangle shape (default Visio master)
            string rectangleMasterName = "Rectangle";

            // Add and position each rectangle
            for (int i = 0; i < shapeCount; i++)
            {
                // Calculate the center X coordinate for the current rectangle
                double centerX = spacing * (i + 1) + rectWidth / 2.0 + rectWidth * i;

                // Add the shape to the page; the fourth argument indicates whether to calculate geometry (false)
                long shapeId = page.AddShape(centerX, centerY, rectangleMasterName, false);

                // Retrieve the shape object using its ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // Set the shape's width and height
                shape.XForm.Width.Value = rectWidth;
                shape.XForm.Height.Value = rectHeight;

                // Ensure the shape's PinX and PinY are set to the calculated center positions
                shape.XForm.PinX.Value = centerX;
                shape.XForm.PinY.Value = centerY;
            }

            // Save the diagram to the specified file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}