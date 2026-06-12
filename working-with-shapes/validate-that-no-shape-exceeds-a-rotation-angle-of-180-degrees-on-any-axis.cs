using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class ValidateShapeRotations
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            var diagram = new Diagram("input.vsdx");

            // Collect shapes that violate the rotation rule
            var invalidShapes = new List<string>();

            // Iterate through all pages and their shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve rotation angles; default to 0 if not set
                    double rotX = shape.ThreeDFormat?.RotationXAngle?.Value ?? 0;
                    double rotY = shape.ThreeDFormat?.RotationYAngle?.Value ?? 0;
                    double rotZ = shape.ThreeDFormat?.RotationZAngle?.Value ?? 0;

                    // Validate that no angle exceeds 180 degrees (absolute value)
                    if (Math.Abs(rotX) > 180 || Math.Abs(rotY) > 180 || Math.Abs(rotZ) > 180)
                    {
                        invalidShapes.Add($"Page: {page.Name}, Shape ID: {shape.ID}");
                    }
                }
            }

            // Output validation results
            if (invalidShapes.Count == 0)
            {
                Console.WriteLine("All shapes have rotation angles within 180 degrees.");
            }
            else
            {
                Console.WriteLine("Shapes exceeding 180-degree rotation:");
                foreach (string info in invalidShapes)
                {
                    Console.WriteLine(info);
                }
            }

            // Save the diagram (unchanged)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
