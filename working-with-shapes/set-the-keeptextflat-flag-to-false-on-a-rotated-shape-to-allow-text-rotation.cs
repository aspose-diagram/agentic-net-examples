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

            // Load an existing Visio diagram (replace with your actual file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Add a rectangle shape to the page (master name "Rectangle")
            // The method returns the shape ID (long)
            long shapeId = page.AddShape(2.0, 2.0, "Rectangle");

            // Retrieve the Shape object using the returned ID
            Shape shape = page.Shapes.GetShape((int)shapeId);

            // Rotate the shape by 45 degrees (SetAngle expects radians)
            double angleDegrees = 45.0;
            double angleRadians = (Math.PI / 180.0) * angleDegrees;
            shape.SetAngle(angleRadians);

            // Disable KeepTextFlat to allow the text to rotate with the shape
            shape.ThreeDFormat.KeepTextFlat.Value = BOOL.False;

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
