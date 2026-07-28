using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Retrieve the shape with ID 3 from the first page
            Shape shape = diagram.Pages[0].Shapes.GetShape(3L);

            // Rotate the text inside the shape by 45 degrees.
            // TextXForm.TxtAngle expects radians, so convert degrees to radians.
            double angleDegrees = 45.0;
            double angleRadians = (Math.PI / 180.0) * angleDegrees;
            shape.TextXForm.TxtAngle.Value = angleRadians;

            // Save the modified diagram (replace with your desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
