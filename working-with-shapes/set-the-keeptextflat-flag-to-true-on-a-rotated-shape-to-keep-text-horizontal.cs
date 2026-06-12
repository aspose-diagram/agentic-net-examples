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

            // Access the first page
            Page page = diagram.Pages[0];

            // Locate the first non‑deleted shape on the page
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Del == BOOL.False)
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                throw new Exception("No shape found in the diagram.");
            }

            // Rotate the shape by 45 degrees (SetAngle expects radians)
            double angleDeg = 45;
            double angleRad = (Math.PI / 180) * angleDeg;
            targetShape.SetAngle(angleRad);

            // Keep the text horizontal despite the rotation
            targetShape.ThreeDFormat.KeepTextFlat.Value = BOOL.True;

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
