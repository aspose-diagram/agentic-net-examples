using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load an existing Visio diagram
            Aspose.Diagram.Diagram diagram = new Aspose.Diagram.Diagram("input.vsdx");

            // Access a specific shape (e.g., first shape on the first page)
            Aspose.Diagram.Shape shape = diagram.Pages[0].Shapes[1];

            // Ensure the shape has a Fill object (it always does) and a GradientFill object
            Aspose.Diagram.GradientFill gradientFill = shape.Fill.GradientFill;

            // Set the gradient angle to 0 degrees (horizontal fill)
            // GradientAngle is a DoubleValue; assign its Value property
            gradientFill.GradientAngle.Value = 0;

            // Optionally enable the gradient if it was disabled
            // gradientFill.GradientEnabled.Value = true;

            // Save the modified diagram
            diagram.Save("output.vsdx", Aspose.Diagram.SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
