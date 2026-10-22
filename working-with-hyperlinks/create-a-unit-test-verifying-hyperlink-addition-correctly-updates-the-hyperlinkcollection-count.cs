using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // No external input files are required for this test, so no file existence guard is needed.

        try
        {
            // Create a new empty diagram.
            Diagram diagram = new Diagram();

            // Create a new page instance and add it to the diagram's page collection.
            Page newPage = new Page();
            diagram.Pages.Add(newPage);

            // Retrieve the page we just added (it is the first and only page).
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the page at coordinates (1.0, 1.0).
            // The AddShape method returns the shape's unique identifier.
            long shapeId = page.AddShape(1.0, 1.0, "Rectangle");

            // Retrieve the shape object using the returned identifier.
            Shape shape = page.Shapes.GetShape(shapeId);

            // Record the initial number of hyperlinks associated with the shape.
            int initialCount = shape.Hyperlinks.Count;

            // Create a new hyperlink and configure its properties.
            Hyperlink link = new Hyperlink();
            link.Name = "TestLink";
            link.Address.Value = "https://example.com";

            // Add the hyperlink to the shape's Hyperlinks collection.
            shape.Hyperlinks.Add(link);

            // Record the count after adding the hyperlink.
            int afterCount = shape.Hyperlinks.Count;

            // Verify that the count increased by exactly one.
            if (afterCount != initialCount + 1)
            {
                throw new Exception($"Hyperlink count mismatch. Expected {initialCount + 1}, but got {afterCount}.");
            }

            // If verification passes, output a success message.
            Console.WriteLine("Hyperlink addition test passed.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error output stream.
            Console.Error.WriteLine("Error: " + ex.Message);
        }
    }
}