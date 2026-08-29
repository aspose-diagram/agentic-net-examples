using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Access the first page
            Page page = diagram.Pages[0];

            // Locate the first shape that contains text
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (!string.IsNullOrWhiteSpace(shape.Text.Value.ToString()))
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.WriteLine("No shape with text found in the diagram.");
                return;
            }

            // Rotate the shape's text by 45 degrees (convert degrees to radians)
            double angleDeg = 45.0;
            double angleRad = (Math.PI / 180.0) * angleDeg;
            targetShape.TextXForm.TxtAngle.Value = angleRad;

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Text rotation applied and diagram saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
