using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file
            string filePath = "input.vsdx";

            // Universal name of the shape to inspect
            string targetShapeNameU = "MyShape";

            // Expected fill background color (hex string)
            const string ExpectedFillColor = "#FF0000";

            // Load the diagram
            Diagram diagram = new Diagram(filePath);

            // Get the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // Locate the shape by its universal name
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.NameU == targetShapeNameU)
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                throw new Exception($"Shape with NameU '{targetShapeNameU}' not found.");
            }

            // Read the fill background color
            string actualFillColor = targetShape.Fill.FillBkgnd.Value;

            // Compare with the predefined constant
            if (string.Equals(actualFillColor, ExpectedFillColor, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Fill background color matches the expected value.");
            }
            else
            {
                Console.WriteLine($"Fill background color '{actualFillColor}' does not match expected '{ExpectedFillColor}'.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
