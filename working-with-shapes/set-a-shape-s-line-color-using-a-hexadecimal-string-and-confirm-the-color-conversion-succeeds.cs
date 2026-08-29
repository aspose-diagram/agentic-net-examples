using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add a rectangle shape to the first page (page index 0)
            // The AddShape method returns the shape ID (long)
            long shapeId = diagram.AddShape(2.0, 2.0, "Rectangle", 0);

            // Retrieve the concrete Shape object using the ID
            Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

            // Set the line color using a hexadecimal string
            string expectedHex = "#FF0000"; // Red
            shape.Line.LineColor.Value = expectedHex;

            // Verify that the color was set correctly
            string actualHex = shape.Line.LineColor.Value;
            if (!string.Equals(actualHex, expectedHex, StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception($"Line color conversion failed. Expected: {expectedHex}, Actual: {actualHex}");
            }
            else
            {
                Console.WriteLine($"Line color successfully set to {actualHex}");
            }

            // Save the diagram as a PNG image to confirm the shape is rendered
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            diagram.Save("output.png", saveOptions);
            Console.WriteLine("Diagram saved as output.png");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
