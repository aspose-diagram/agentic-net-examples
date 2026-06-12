using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    // Tolerance for dimension comparison (in inches)
    private const double Tolerance = 0.001;

    public static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Access the first (default) page
        Page page = diagram.Pages[0];

        // Draw an initial rectangle shape (pinX, pinY, width, height)
        // Initial dimensions are arbitrary; they will be changed by the test
        double initialPinX = 2.0;
        double initialPinY = 2.0;
        double initialWidth = 1.0;
        double initialHeight = 1.0;
        long shapeId = page.DrawRectangle(initialPinX, initialPinY, initialWidth, initialHeight);

        // Retrieve the shape instance
        Shape shape = page.Shapes.GetShape(shapeId);

        // Desired dimensions for the test
        double expectedWidth = 2.5;
        double expectedHeight = 1.75;

        // Apply new dimensions using SetWidth and SetHeight
        shape.SetWidth(expectedWidth);
        shape.SetHeight(expectedHeight);

        // Read back the actual dimensions from the shape's XForm cells
        double actualWidth = shape.XForm.Width.Value;
        double actualHeight = shape.XForm.Height.Value;

        // Verify width within tolerance
        if (Math.Abs(actualWidth - expectedWidth) > Tolerance)
        {
            throw new Exception($"SetWidth failed: expected {expectedWidth}, actual {actualWidth}");
        }
        else
        {
            Console.WriteLine($"SetWidth passed: expected {expectedWidth}, actual {actualWidth}");
        }

        // Verify height within tolerance
        if (Math.Abs(actualHeight - expectedHeight) > Tolerance)
        {
            throw new Exception($"SetHeight failed: expected {expectedHeight}, actual {actualHeight}");
        }
        else
        {
            Console.WriteLine($"SetHeight passed: expected {expectedHeight}, actual {actualHeight}");
        }

        // Optional: save the diagram to verify no runtime errors (not required for the test)
        // diagram.Save("SetDimensionsTest.vsdx", SaveFileFormat.Vsdx);
    }
}
