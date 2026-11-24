using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    // Predefined constant for the expected fill background color (hex string)
    private const string ExpectedFillBackgroundColor = "#FF0000";

    static void Main(string[] args)
    {
        // Determine the diagram file path (first argument or default)
        string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at path '{diagramPath}'.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Ensure the diagram contains at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Error: The diagram does not contain any pages.");
                return;
            }

            // Get the first page (index 0)
            Page page = diagram.Pages[0];

            // Ensure the page contains at least one shape
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("Error: The page does not contain any shapes.");
                return;
            }

            // Retrieve a shape by its ID.
            // Here we use shape ID 1 as an example; adjust as needed.
            long shapeId = 1L;
            Shape shape = page.Shapes.GetShape(shapeId);

            // Guard: check if the shape was found
            if (shape == null)
            {
                Console.Error.WriteLine($"Error: Shape with ID {shapeId} not found on the page.");
                return;
            }

            // Read the fill background color value
            // According to Aspose.Diagram API, the property is Fill.FillBkgnd.Value
            string actualFillColor = shape.Fill?.FillBkgnd?.Value;

            // Guard: ensure the fill background color value is available
            if (string.IsNullOrEmpty(actualFillColor))
            {
                Console.Error.WriteLine("Error: Unable to retrieve the fill background color of the shape.");
                return;
            }

            // Compare the actual fill color with the predefined constant (case-insensitive)
            if (string.Equals(actualFillColor, ExpectedFillBackgroundColor, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Success: Shape fill background color matches the expected value '{ExpectedFillBackgroundColor}'.");
            }
            else
            {
                Console.WriteLine($"Mismatch: Shape fill background color is '{actualFillColor}', expected '{ExpectedFillBackgroundColor}'.");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related exceptions and output the error
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}