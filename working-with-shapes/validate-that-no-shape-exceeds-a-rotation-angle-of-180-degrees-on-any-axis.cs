using System.IO;
using System;
using Aspose.Diagram;

class ValidateShapeRotation
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            bool violationFound = false;

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve rotation angles; if a 3D format is not defined, default to 0
                    double rotX = shape.ThreeDFormat?.RotationXAngle?.Value ?? 0;
                    double rotY = shape.ThreeDFormat?.RotationYAngle?.Value ?? 0;
                    double rotZ = shape.ThreeDFormat?.RotationZAngle?.Value ?? 0;

                    // Check if any axis exceeds ±180 degrees
                    if (Math.Abs(rotX) > 180 || Math.Abs(rotY) > 180 || Math.Abs(rotZ) > 180)
                    {
                        violationFound = true;
                        Console.WriteLine(
                            $"Violation: Shape ID {shape.ID} on page \"{page.Name}\" exceeds rotation limit. " +
                            $"X={rotX}, Y={rotY}, Z={rotZ}");
                    }
                }
            }

            if (!violationFound)
            {
                Console.WriteLine("All shapes are within the 180-degree rotation limit on every axis.");
            }

            // Save the diagram (optional, preserves original if no changes were made)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
