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

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Identify the page and shape to modify
            // Here we use the first page and a shape with a known ID (replace with actual ID as needed)
            Page page = diagram.Pages[0];
            long shapeId = 1; // <-- replace with the target shape ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Desired text margins in pixels
            int leftPixels = 10;
            int rightPixels = 15;
            int topPixels = 8;
            int bottomPixels = 12;

            // Convert pixels to points (1 pixel = 0.75 points at 96 DPI)
            double leftPoints = leftPixels * 0.75;
            double rightPoints = rightPixels * 0.75;
            double topPoints = topPixels * 0.75;
            double bottomPoints = bottomPixels * 0.75;

            // Apply the margins to the shape's TextBlock (units are points)
            shape.TextBlock.LeftMargin = new DoubleValue(leftPoints, MeasureConst.PT);
            shape.TextBlock.RightMargin = new DoubleValue(rightPoints, MeasureConst.PT);
            shape.TextBlock.TopMargin = new DoubleValue(topPoints, MeasureConst.PT);
            shape.TextBlock.BottomMargin = new DoubleValue(bottomPoints, MeasureConst.PT);

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
