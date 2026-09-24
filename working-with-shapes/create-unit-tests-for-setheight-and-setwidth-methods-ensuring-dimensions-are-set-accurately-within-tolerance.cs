using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    // Tolerance for floating point comparison
    private const double Tolerance = 0.001;

    static void Main()
    {
        try
        {
            TestSetHeight(); // Run height test
            TestSetWidth();  // Run width test
            Console.WriteLine("All dimension tests passed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Test failed: {ex.Message}");
            // Re‑throw to indicate failure in a real test runner
            throw;
        }
    }

    // Test that SetHeight correctly updates the shape's Height within tolerance
    private static void TestSetHeight()
    {
        // Create a new empty diagram and add a page
        Diagram diagram = new Diagram();
        diagram.Pages.Add(new Page());
        Page page = diagram.Pages[0];

        // Add a rectangle shape (initial size does not matter)
        // Cast literals to float because DrawRectangle expects float parameters
        long shapeId = page.DrawRectangle(pinX: 2.0f, pinY: 2.0f, width: 3.0f, height: 3.0f);
        Shape shape = page.Shapes.GetShape(shapeId);

        // Desired new height
        double newHeight = 5.123;

        // Apply the height change
        shape.SetHeight(newHeight);

        // Verify the height was set correctly
        double actualHeight = shape.XForm.Height.Value;
        if (Math.Abs(actualHeight - newHeight) > Tolerance)
        {
            throw new Exception($"SetHeight test failed. Expected {newHeight}, but got {actualHeight}.");
        }

        Console.WriteLine($"SetHeight test passed. Height = {actualHeight}");
    }

    // Test that SetWidth correctly updates the shape's Width within tolerance
    private static void TestSetWidth()
    {
        // Create a new empty diagram and add a page
        Diagram diagram = new Diagram();
        diagram.Pages.Add(new Page());
        Page page = diagram.Pages[0];

        // Add a rectangle shape (initial size does not matter)
        // Cast literals to float because DrawRectangle expects float parameters
        long shapeId = page.DrawRectangle(pinX: 1.0f, pinY: 1.0f, width: 2.0f, height: 2.0f);
        Shape shape = page.Shapes.GetShape(shapeId);

        // Desired new width
        double newWidth = 7.456;

        // Apply the width change
        shape.SetWidth(newWidth);

        // Verify the width was set correctly
        double actualWidth = shape.XForm.Width.Value;
        if (Math.Abs(actualWidth - newWidth) > Tolerance)
        {
            throw new Exception($"SetWidth test failed. Expected {newWidth}, but got {actualWidth}.");
        }

        Console.WriteLine($"SetWidth test passed. Width = {actualWidth}");
    }
}