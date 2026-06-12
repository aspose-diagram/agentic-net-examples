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

            // Access the first page in the diagram
            Page page = diagram.Pages[0];

            // Find the first shape on the page (if any)
            Shape? targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                targetShape = shape;
                break;
            }

            if (targetShape != null)
            {
                // Rotate the shape's text by 45 degrees.
                // TxtAngle expects radians, so convert degrees to radians.
                double angleDegrees = 45.0;
                double angleRadians = (Math.PI / 180.0) * angleDegrees;
                targetShape.TextXForm.TxtAngle.Value = angleRadians;
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
