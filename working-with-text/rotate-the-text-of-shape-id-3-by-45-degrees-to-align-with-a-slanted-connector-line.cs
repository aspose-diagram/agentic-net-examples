using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your actual file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the first page (index 0)
            Page page = diagram.Pages[0];

            // Retrieve shape with ID 3
            Shape shape = page.Shapes.GetShape(3);

            // Ensure the shape is not marked as deleted
            if (shape.Del == BOOL.False)
            {
                // Rotate the text within the shape by 45 degrees.
                // Text rotation uses radians, so convert degrees to radians.
                double angleDeg = 45.0;
                double angleRad = (Math.PI / 180.0) * angleDeg;
                shape.TextXForm.TxtAngle.Value = angleRad;
            }
            else
            {
                Console.WriteLine("Shape with ID 3 is deleted and cannot be modified.");
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Text rotation applied and diagram saved to " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
