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

            // Access the first page (index 0)
            Page page = diagram.Pages[0];

            // Retrieve a shape by its ID (example: ID = 1)
            // Adjust the ID as needed for your diagram
            Shape shape = page.Shapes.GetShape(1);

            // Rotate the shape by 90 degrees (angle in radians)
            double angleRadians = Math.PI / 2; // 90 degrees
            shape.SetAngle(angleRadians);

            // Disable KeepTextFlat to allow text rotation on the shape
            shape.ThreeDFormat.KeepTextFlat.Value = BOOL.False;

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Shape rotated and KeepTextFlat set to false. Diagram saved to " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
