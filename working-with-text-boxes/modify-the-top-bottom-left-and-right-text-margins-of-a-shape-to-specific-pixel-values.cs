using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Convert pixel value to points (1 pixel = 0.75 point at 96 DPI)
    static double PixelsToPoints(int pixels) => pixels * 0.75;

    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Define desired pixel margins
            int leftPixels = 10;
            int rightPixels = 10;
            int topPixels = 5;
            int bottomPixels = 5;

            // Convert pixel margins to points
            double leftPoints = PixelsToPoints(leftPixels);
            double rightPoints = PixelsToPoints(rightPixels);
            double topPoints = PixelsToPoints(topPixels);
            double bottomPoints = PixelsToPoints(bottomPixels);

            // Iterate through pages and shapes (example modifies the first shape found)
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a TextBlock (all shapes have it, but check for null safety)
                    if (shape.TextBlock != null)
                    {
                        // Set margins using DoubleValue with point units
                        shape.TextBlock.LeftMargin = new DoubleValue(leftPoints, MeasureConst.PT);
                        shape.TextBlock.RightMargin = new DoubleValue(rightPoints, MeasureConst.PT);
                        shape.TextBlock.TopMargin = new DoubleValue(topPoints, MeasureConst.PT);
                        shape.TextBlock.BottomMargin = new DoubleValue(bottomPoints, MeasureConst.PT);
                    }

                    // Break after first shape if only one shape needs modification
                    // Remove the break statement to apply to all shapes
                    break;
                }

                // Break after first page if only one page needs modification
                // Remove the break statement to apply to all pages
                break;
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
