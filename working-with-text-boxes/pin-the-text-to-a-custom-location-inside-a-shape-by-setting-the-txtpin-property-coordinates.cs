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

            // Access the first (default) page
            Page page = diagram.Pages[0];

            // Add a rectangle shape at PinX=2, PinY=2
            long shapeId = page.AddShape(2.0, 2.0, "Rectangle");

            // Retrieve the shape instance using its ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Clear any existing text and add new text
            shape.Text.Value.Clear();
            shape.Text.Value.Add(new Txt("Pinned Text"));

            // Set custom text pin coordinates inside the shape
            // These coordinates are relative to the shape's local coordinate system
            shape.TextXForm.TxtPinX.Value = 0.5; // X offset
            shape.TextXForm.TxtPinY.Value = 0.2; // Y offset

            // Save the diagram to a VSDX file
            diagram.Save("PinnedTextDiagram.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
