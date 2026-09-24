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

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Get the first page (index 0)
            Page page = diagram.Pages[0];

            // Retrieve the first shape on the page
            // Shape IDs are returned as long; cast to int for GetShape
            long shapeId = page.Shapes[0].ID;
            Shape shape = page.Shapes.GetShape(shapeId);

            // Rotate the shape's text by 45 degrees.
            // TxtAngle is in radians, so convert degrees to radians.
            double angleDegrees = 45.0;
            double angleRadians = (Math.PI / 180.0) * angleDegrees;
            shape.TextXForm.TxtAngle.Value = angleRadians;

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
