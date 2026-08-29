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
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Choose the page (first page in this example)
                Page page = diagram.Pages[0];

                // Retrieve the shape to modify.
                // Here we assume the shape ID is known (e.g., 1). Adjust as needed.
                long shapeId = 1;
                Shape shape = page.Shapes.GetShape(shapeId);

                // Desired margins in pixels
                double leftPixels = 10;
                double rightPixels = 10;
                double topPixels = 5;
                double bottomPixels = 5;

                // Convert pixels to inches (96 pixels = 1 inch)
                double leftInches = leftPixels / 96.0;
                double rightInches = rightPixels / 96.0;
                double topInches = topPixels / 96.0;
                double bottomInches = bottomPixels / 96.0;

                // Apply the margins to the shape's TextBlock
                shape.TextBlock.LeftMargin = new DoubleValue(leftInches, MeasureConst.IN);
                shape.TextBlock.RightMargin = new DoubleValue(rightInches, MeasureConst.IN);
                shape.TextBlock.TopMargin = new DoubleValue(topInches, MeasureConst.IN);
                shape.TextBlock.BottomMargin = new DoubleValue(bottomInches, MeasureConst.IN);

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