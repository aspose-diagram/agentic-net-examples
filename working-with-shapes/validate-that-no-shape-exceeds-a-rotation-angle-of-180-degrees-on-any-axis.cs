using System.IO;
using System;
using Aspose.Diagram;

class RotationValidator
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // ThreeDFormat may be null if the shape has no 3‑D properties
                    ThreeDFormat threeD = shape.ThreeDFormat;
                    if (threeD == null) continue;

                    // Retrieve rotation angles; DoubleValue may be null
                    double rotX = threeD.RotationXAngle?.Value ?? 0.0;
                    double rotY = threeD.RotationYAngle?.Value ?? 0.0;
                    double rotZ = threeD.RotationZAngle?.Value ?? 0.0;

                    // Validate each axis does not exceed 180 degrees
                    if (Math.Abs(rotX) > 180.0 ||
                        Math.Abs(rotY) > 180.0 ||
                        Math.Abs(rotZ) > 180.0)
                    {
                        Console.WriteLine($"Shape ID {shape.ID} on page '{page.Name}' exceeds rotation limits:");
                        Console.WriteLine($"  RotationXAngle = {rotX}");
                        Console.WriteLine($"  RotationYAngle = {rotY}");
                        Console.WriteLine($"  RotationZAngle = {rotZ}");
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
