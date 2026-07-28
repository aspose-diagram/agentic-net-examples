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

            // Create a new diagram
            Diagram diagram = new Diagram();

            // Access the first page
            Page page = diagram.Pages[0];

            // Add a rectangle shape (master name "Rectangle") at (2,2) with size 1x1 on page index 0
            long shapeId = diagram.AddShape(2.0, 2.0, 1.0, 1.0, "Rectangle", 0);

            // Retrieve the shape object using its ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Configure the 3D rotation type to Perspective
            shape.ThreeDFormat.RotationType.Value = RotationTypeValue.Perspective;

            // Assign a 45‑degree perspective angle
            shape.ThreeDFormat.Perspective.Value = 45;

            // Save the diagram to a VSDX file
            diagram.Save("RotatedShape.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
