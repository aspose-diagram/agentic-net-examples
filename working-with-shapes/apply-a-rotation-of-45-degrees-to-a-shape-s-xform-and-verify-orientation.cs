using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Paths for input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram from file
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Find the first non‑deleted shape on the page
                Shape targetShape = null;
                foreach (Shape shp in page.Shapes)
                {
                    if (shp.Del == BOOL.False)
                    {
                        targetShape = shp;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    throw new Exception("No suitable shape found on the page.");
                }

                // Rotate the shape by 45 degrees (convert to radians)
                double angleDeg = 45.0;
                double angleRad = (Math.PI / 180.0) * angleDeg;
                targetShape.XForm.Angle.Value = angleRad;

                // Verify that the rotation was applied correctly
                double actualRad = targetShape.XForm.Angle.Value;
                double tolerance = 0.0001; // acceptable deviation

                if (Math.Abs(actualRad - angleRad) > tolerance)
                {
                    throw new Exception($"Rotation verification failed. Expected {angleRad} rad, but got {actualRad} rad.");
                }
                else
                {
                    Console.WriteLine("Shape rotated 45 degrees successfully.");
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
