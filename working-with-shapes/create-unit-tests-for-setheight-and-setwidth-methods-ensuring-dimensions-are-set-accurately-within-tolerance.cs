using System;
using System.IO;
using Aspose.Diagram;

public class Program
{
    // Tolerance for dimension comparison
    private const double Tolerance = 0.001;

    public static void Main()
    {
        try
        {
            TestSetWidthAndHeight();
            Console.WriteLine("All SetWidth/SetHeight tests passed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Test failed: {ex.Message}");
            throw;
        }
    }

    private static void TestSetWidthAndHeight()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Use the first (default) page
        Page page = diagram.Pages[0];

        // Initial dimensions (center point and desired size)
        double initialPinX = 5.0;
        double initialPinY = 5.0;
        double initialWidth = 2.0;
        double initialHeight = 1.0;

        // DrawRectangle expects opposite corner coordinates, not width/height.
        // Compute corner coordinates by adding width/height to the pin point.
        long shapeId = page.DrawRectangle(
            initialPinX,
            initialPinY,
            initialPinX + initialWidth,
            initialPinY + initialHeight);

        // Retrieve the created shape by its ID
        Shape shape = page.Shapes.GetShape(shapeId);

        // Verify initial dimensions match the intended size
        VerifyDimension(shape.XForm.Width.Value, initialWidth, "Initial Width");
        VerifyDimension(shape.XForm.Height.Value, initialHeight, "Initial Height");

        // New dimensions to set
        double newWidth = 3.456;
        double newHeight = 2.789;

        // Apply new dimensions using SetWidth and SetHeight
        shape.SetWidth(newWidth);
        shape.SetHeight(newHeight);

        // Verify that dimensions were updated within tolerance
        VerifyDimension(shape.XForm.Width.Value, newWidth, "Updated Width");
        VerifyDimension(shape.XForm.Height.Value, newHeight, "Updated Height");
    }

    private static void VerifyDimension(double actual, double expected, string description)
    {
        double diff = Math.Abs(actual - expected);
        if (diff > Tolerance)
        {
            throw new Exception($"{description} mismatch. Expected: {expected}, Actual: {actual}, Diff: {diff}");
        }
        else
        {
            Console.WriteLine($"{description} verified. Expected: {expected}, Actual: {actual}");
        }
    }
}