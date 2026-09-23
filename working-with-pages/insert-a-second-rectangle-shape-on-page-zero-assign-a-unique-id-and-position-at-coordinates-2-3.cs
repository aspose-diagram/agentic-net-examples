using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Create a new diagram instance
            Diagram diagram = new Diagram();

            // Get the first (zero‑based) page of the diagram
            Page page = diagram.Pages[0];

            // Add a rectangle shape at coordinates (2, 3)
            // Parameters: pinX, pinY, master name, isCalculate flag
            long shapeId = page.AddShape(2.0, 3.0, "Rectangle", false);

            // Retrieve the shape object using its unique ID
            Shape rectangle = page.Shapes.GetShape(shapeId);

            // Ensure the shape is positioned at the desired coordinates
            rectangle.XForm.PinX.Value = 2.0;
            rectangle.XForm.PinY.Value = 3.0;

            // Save the diagram to a VSDX file
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
