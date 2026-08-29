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

            // Access the default first page
            Page page = diagram.Pages[0];

            // Add a rectangle shape at (2,2) inches
            long shapeId = page.AddShape(2.0, 2.0, "Rectangle");

            // Retrieve the Shape instance using the returned ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Apply a dashed line pattern to the shape
            shape.Line.LinePattern.Value = LinePatternValue.Dash;

            // (Optional) Set line color and weight for visibility
            shape.Line.LineColor.Value = "#FF0000"; // red color
            shape.Line.LineWeight.Value = 0.02;    // line thickness in inches

            // Save the diagram to a VSDX file
            diagram.Save("DashedShape.vsdx", SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram saved with a shape that has a dashed line style.");

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
