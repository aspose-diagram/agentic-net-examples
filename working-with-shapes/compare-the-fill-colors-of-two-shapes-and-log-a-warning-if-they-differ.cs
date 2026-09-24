using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio diagram file (adjust as needed)
        string diagramPath = "input.vsdx";

        // Guard: ensure the diagram file exists before proceeding
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        // IDs of the two shapes to compare (adjust as needed)
        long shapeId1 = 1;
        long shapeId2 = 2;

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(diagramPath);

            // Retrieve the first page (assuming shapes are on the first page)
            Page page = diagram.Pages[0];

            // Get the first shape by its ID
            Shape shape1 = page.Shapes.GetShape(shapeId1);
            if (shape1 == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId1} not found.");
                return;
            }

            // Get the second shape by its ID
            Shape shape2 = page.Shapes.GetShape(shapeId2);
            if (shape2 == null)
            {
                Console.Error.WriteLine($"Shape with ID {shapeId2} not found.");
                return;
            }

            // Extract the fill foreground color values (hex strings) from both shapes
            string fillColor1 = shape1.Fill.FillForegnd.Value;
            string fillColor2 = shape2.Fill.FillForegnd.Value;

            // Compare the fill colors and log a warning if they differ
            if (!string.Equals(fillColor1, fillColor2, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine($"Warning: Fill colors differ – Shape {shapeId1} = {fillColor1}, Shape {shapeId2} = {fillColor2}");
            }
            else
            {
                Console.WriteLine($"Info: Fill colors are identical – both are {fillColor1}");
            }
        }
        catch (Exception ex)
        {
            // Log any unexpected errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}