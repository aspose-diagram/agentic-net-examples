using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio diagram
            string sourcePath = "input.vsdx";

            // Load the diagram (uses the provided load rule)
            Diagram diagram = new Diagram(sourcePath);

            // Choose the page and shape to rotate
            // Here we use the first page and a shape with a known ID (replace with your actual ID)
            Page page = diagram.Pages[0];
            int shapeId = 1; // TODO: set the actual shape ID you want to rotate
            Shape shape = page.Shapes.GetShape(shapeId);

            // Desired rotation angle in degrees
            double angleDegrees = 45.0;

            // Convert degrees to radians because Shape.SetAngle expects radians
            double angleRadians = angleDegrees * Math.PI / 180.0;

            // Rotate the shape by setting its angle
            // Option 1: using the convenience method
            shape.SetAngle(angleRadians);

            // Option 2: directly modifying the XForm.Angle property
            // shape.XForm.Angle.Value = angleRadians;

            // Save the modified diagram (uses the provided save rule)
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
