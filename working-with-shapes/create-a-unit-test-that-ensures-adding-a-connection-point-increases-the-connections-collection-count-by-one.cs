using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // No external input files are required for this test.

        try
        {
            // Create a new empty diagram.
            Diagram diagram = new Diagram();

            // Ensure there is at least one page to work with.
            if (diagram.Pages.Count == 0)
            {
                // Add a blank page if none exist.
                diagram.Pages.Add(new Page());
            }

            // Use the first page.
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the page.
            // Parameters: pinX, pinY, masterName, isCalculate
            long shapeId = page.AddShape(0.0, 0.0, "Rectangle", false);

            // Retrieve the shape object.
            Shape shape = page.Shapes.GetShape(shapeId);
            if (shape == null)
            {
                Console.Error.WriteLine("Failed to retrieve the newly added shape.");
                return;
            }

            // Record the initial number of connection points.
            int initialCount = shape.Connections.Count;

            // Create a new connection point.
            Connection cp = new Connection();
            // Position the connection point at the middle of the shape's top edge.
            cp.X.Ufe.F = "Width*0.5";
            cp.Y.Ufe.F = "Height*0";

            // Add the connection point to the shape.
            shape.Connections.Add(cp);

            // Record the new number of connection points.
            int newCount = shape.Connections.Count;

            // Verify that the count increased by exactly one.
            if (newCount == initialCount + 1)
            {
                Console.WriteLine("Test passed: Connection point added successfully.");
                Console.WriteLine($"Initial count: {initialCount}, New count: {newCount}");
            }
            else
            {
                throw new Exception($"Test failed: Expected count {initialCount + 1}, but got {newCount}.");
            }
        }
        catch (Exception ex)
        {
            // Output any errors encountered during the test.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}