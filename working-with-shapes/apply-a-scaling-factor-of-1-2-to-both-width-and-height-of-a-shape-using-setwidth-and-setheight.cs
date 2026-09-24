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

            // Add a new page to the diagram
            Page page = new Page();
            diagram.Pages.Add(page);

            // Add a rectangle shape to the page
            // Parameters: pinX, pinY, master name, isCalculate (bool)
            long shapeId = page.AddShape(2.0, 2.0, "Rectangle", false);

            // Retrieve the shape object using its ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Get current width and height
            double currentWidth = shape.XForm.Width.Value;
            double currentHeight = shape.XForm.Height.Value;

            // Apply scaling factor of 1.2 using SetWidth and SetHeight
            double scaleFactor = 1.2;
            shape.SetWidth(currentWidth * scaleFactor);
            shape.SetHeight(currentHeight * scaleFactor);

            // Save the diagram to a VSDX file
            diagram.Save("scaled_shape.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
