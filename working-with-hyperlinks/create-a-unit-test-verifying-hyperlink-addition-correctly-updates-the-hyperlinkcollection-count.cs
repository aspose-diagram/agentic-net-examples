using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Create a new empty diagram.
        Diagram diagram;
        try
        {
            diagram = new Diagram();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create diagram: {ex.Message}");
            return;
        }

        // Ensure the diagram has at least one page.
        if (diagram.Pages == null || diagram.Pages.Count == 0)
        {
            Console.Error.WriteLine("Diagram does not contain any pages.");
            return;
        }

        // Get the first page.
        Page page = diagram.Pages[0];

        // Add a rectangle shape to the page.
        long shapeId;
        try
        {
            // PinX, PinY coordinates and master name for a rectangle.
            shapeId = page.AddShape(2.0, 2.0, "Rectangle");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to add shape: {ex.Message}");
            return;
        }

        // Retrieve the shape instance from the page.
        Shape shape;
        try
        {
            shape = page.Shapes.GetShape(shapeId);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to retrieve shape: {ex.Message}");
            return;
        }

        // Verify the shape was retrieved.
        if (shape == null)
        {
            Console.Error.WriteLine("Shape retrieval returned null.");
            return;
        }

        // Create a new hyperlink instance.
        Hyperlink link = new Hyperlink();
        link.Name = "WebLink";
        link.Address.Value = "https://example.com";

        // Add the hyperlink to the shape's Hyperlinks collection.
        try
        {
            shape.Hyperlinks.Add(link);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to add hyperlink: {ex.Message}");
            return;
        }

        // Verify that the HyperlinkCollection count reflects the addition.
        int expectedCount = 1;
        int actualCount = shape.Hyperlinks != null ? shape.Hyperlinks.Count : 0;

        if (actualCount == expectedCount)
        {
            Console.WriteLine("Test Passed: Hyperlink collection count is correct.");
        }
        else
        {
            Console.Error.WriteLine($"Test Failed: Expected hyperlink count {expectedCount}, but got {actualCount}.");
            // Optionally throw to indicate failure in automated environments.
            throw new Exception("Hyperlink collection count mismatch.");
        }
    }
}