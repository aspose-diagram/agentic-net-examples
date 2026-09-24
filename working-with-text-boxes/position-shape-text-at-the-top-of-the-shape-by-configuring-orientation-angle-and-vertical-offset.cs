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

            // Create a new blank diagram
            Diagram diagram = new Diagram();

            // Get the first (and only) page
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the page
            // Parameters: PinX, PinY, master name, isCalculate (false)
            long shapeId = page.AddShape(2.0, 2.0, "Rectangle", false);

            // Retrieve the shape object using its ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Clear any existing text and add new text
            shape.Text.Value.Clear();
            shape.Text.Value.Add(new Txt("Sample Text"));

            // Position the text at the top of the shape
            // TxtLocPinY = 0 aligns the text block's local pin to the top edge of the text block
            // TxtPinY = shape height places the text block at the top edge of the shape
            shape.TextXForm.TxtLocPinY.Value = 0;
            shape.TextXForm.TxtPinY.Value = shape.XForm.Height.Value;

            // Set text orientation angle (0 radians = no rotation)
            shape.TextXForm.TxtAngle.Value = 0;

            // Save the diagram to a VSDX file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
