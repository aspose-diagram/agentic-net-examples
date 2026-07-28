using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Create a new diagram instance
            Diagram diagram = new Diagram();

            // Add a rectangle shape to the active page at (2,2) inches
            long shapeId = diagram.ActivePage.AddShape(2.0, 2.0, "Rectangle");

            // Retrieve the shape object using its ID
            Shape shape = diagram.ActivePage.Shapes.GetShape(shapeId);

            // Replace any existing text with new content
            shape.Text.Value.Clear();
            shape.Text.Value.Add(new Txt("Custom positioned text"));

            // Pin the text to a custom location inside the shape
            // Coordinates are in inches relative to the shape's origin
            shape.TextXForm.TxtPinX.Value = 0.5; // X offset
            shape.TextXForm.TxtPinY.Value = 0.2; // Y offset

            // Save the diagram to a VSDX file
            diagram.Save("PinnedTextDiagram.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.NullReferenceException ex)
        {
            Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
        }
    }
}
