using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Define the shape ID to modify (example: first shape on the first page)
            Page page = diagram.Pages[0];
            // Retrieve the shape by its ID (replace 1 with the actual shape ID)
            Shape shape = page.Shapes.GetShape(1);

            // Pixel values for margins
            int topPixels = 10;
            int bottomPixels = 10;
            int leftPixels = 5;
            int rightPixels = 5;

            // Convert pixels to points (1 pixel = 0.75 point at 96 DPI)
            double topPoints = topPixels * 0.75;
            double bottomPoints = bottomPixels * 0.75;
            double leftPoints = leftPixels * 0.75;
            double rightPoints = rightPixels * 0.75;

            // Set the text block margins using DoubleValue with point units
            shape.TextBlock.TopMargin = new DoubleValue(topPoints, MeasureConst.PT);
            shape.TextBlock.BottomMargin = new DoubleValue(bottomPoints, MeasureConst.PT);
            shape.TextBlock.LeftMargin = new DoubleValue(leftPoints, MeasureConst.PT);
            shape.TextBlock.RightMargin = new DoubleValue(rightPoints, MeasureConst.PT);

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Clean up
            diagram.Dispose();

            Console.WriteLine("Margins updated and diagram saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
