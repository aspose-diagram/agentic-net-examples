using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (index 0)
            Page page = diagram.Pages[0];

            // Retrieve the shape with ID 3
            Shape shape = page.Shapes.GetShape(3);

            // Rotate the shape's text by 45 degrees (TextXForm uses radians)
            double angleDeg = 45;
            shape.TextXForm.TxtAngle.Value = (Math.PI / 180) * angleDeg;

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
