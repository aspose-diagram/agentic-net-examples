using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Access the first (default) page of the diagram
            Page page = diagram.Pages[0];

            // Add three rectangle shapes at the same initial location (0,0) with size 1x1 inches
            long id1 = page.DrawRectangle(0, 0, 1, 1);
            long id2 = page.DrawRectangle(0, 0, 1, 1);
            long id3 = page.DrawRectangle(0, 0, 1, 1);

            // Retrieve the shape objects using their IDs
            Shape shape1 = page.Shapes.GetShape(id1);
            Shape shape2 = page.Shapes.GetShape(id2);
            Shape shape3 = page.Shapes.GetShape(id3);

            // Configure auto‑spacing options: 2 inches horizontally and vertically
            AutoSpaceOptions options = new AutoSpaceOptions();
            options.DistanceInHorizontal = 2; // horizontal spacing in inches
            options.DistanceInVertical = 2;   // vertical spacing in inches

            // Apply auto‑spacing to all shapes on the page
            page.AutoSpaceShapes(page.Shapes, options);

            // Capture the resulting PinX and PinY coordinates after spacing
            double pinX1 = shape1.XForm.PinX.Value;
            double pinX2 = shape2.XForm.PinX.Value;
            double pinX3 = shape3.XForm.PinX.Value;

            double pinY1 = shape1.XForm.PinY.Value;
            double pinY2 = shape2.XForm.PinY.Value;
            double pinY3 = shape3.XForm.PinY.Value;

            // Compute absolute distances between consecutive shapes (horizontal)
            double dist12X = Math.Abs(pinX2 - pinX1);
            double dist23X = Math.Abs(pinX3 - pinX2);

            // Compute absolute distances between consecutive shapes (vertical)
            double dist12Y = Math.Abs(pinY2 - pinY1);
            double dist23Y = Math.Abs(pinY3 - pinY2);

            // Verify horizontal spacing meets or exceeds the specified distance
            if (dist12X < options.DistanceInHorizontal || dist23X < options.DistanceInHorizontal)
                throw new Exception($"Horizontal spacing validation failed: distances are {dist12X} and {dist23X} inches.");

            // Verify vertical spacing meets or exceeds the specified distance
            if (dist12Y < options.DistanceInVertical || dist23Y < options.DistanceInVertical)
                throw new Exception($"Vertical spacing validation failed: distances are {dist12Y} and {dist23Y} inches.");

            // If no exception was thrown, the spacing is as expected
            Console.WriteLine("Auto‑spacing validation succeeded.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}