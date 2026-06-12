using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Retrieve the first shape on the page (if any)
            Shape targetShape = null;
            foreach (Shape s in page.Shapes)
            {
                targetShape = s;
                break;
            }

            if (targetShape == null)
            {
                Console.WriteLine("No shapes found on the first page.");
                return;
            }

            // Apply combined rotations of 30 degrees on X, Y, and Z axes
            targetShape.ThreeDFormat.RotationXAngle.Value = 30;
            targetShape.ThreeDFormat.RotationYAngle.Value = 30;
            targetShape.ThreeDFormat.RotationZAngle.Value = 30;

            // Save the modified diagram to a new file
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Shape rotated on X, Y, Z axes by 30 degrees and saved to " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
