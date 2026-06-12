using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Add a rectangle shape to the active page at position (2,2)
            long shapeId = diagram.ActivePage.AddShape(2.0, 2.0, "Rectangle");

            // Retrieve the concrete Shape object using the returned ID
            Shape shape = diagram.ActivePage.Shapes.GetShape((int)shapeId);

            // Set the line color using a hexadecimal string
            string expectedColor = "#00FF00"; // green
            shape.Line.LineColor.Value = expectedColor;

            // Verify that the color was applied correctly
            if (shape.Line.LineColor.Value != expectedColor)
            {
                throw new Exception("Line color conversion failed.");
            }
            else
            {
                Console.WriteLine("Line color set and verified successfully: " + shape.Line.LineColor.Value);
            }

            // Save the diagram to a file (optional, demonstrates proper save usage)
            diagram.Save("LineColorDemo.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
