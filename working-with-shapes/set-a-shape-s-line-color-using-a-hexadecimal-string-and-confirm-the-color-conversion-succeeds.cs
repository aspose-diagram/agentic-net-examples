using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new diagram (empty)
            Diagram diagram = new Diagram();

            // Ensure there is at least one page
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the page
            // Parameters: pinX, pinY, width, height, master name, isCalculate
            long shapeId = page.AddShape(2.0, 2.0, 1.0, 0.5, "Rectangle", false);

            // Retrieve the shape object
            Shape shape = page.Shapes.GetShape(shapeId);

            // Set the line color using a hexadecimal string
            string hexColor = "#FF0000"; // Red
            shape.Line.LineColor.Value = hexColor;

            // Verify that the color was set correctly
            if (shape.Line.LineColor.Value != hexColor)
            {
                throw new Exception($"Line color conversion failed. Expected {hexColor}, but got {shape.Line.LineColor.Value}.");
            }
            else
            {
                Console.WriteLine($"Line color successfully set to {hexColor}.");
            }

            // Save the diagram to verify persistence (optional)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
