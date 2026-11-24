using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the Visio file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string filePath = args[0];

        // Guard: verify the file exists before proceeding.
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"Error: File not found - {filePath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(filePath);

            bool anyInvalid = false;

            // Iterate through all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve the 2D rotation angle (in degrees).
                    double angle2D = shape.XForm.Angle.Value;

                    // Retrieve 3D rotation angles (in degrees) from the ThreeDFormat section.
                    double rotX = shape.ThreeDFormat.RotationXAngle.Value;
                    double rotY = shape.ThreeDFormat.RotationYAngle.Value;
                    double rotZ = shape.ThreeDFormat.RotationZAngle.Value;

                    // Validate each angle does not exceed 180 degrees.
                    if (angle2D > 180.0 ||
                        rotX > 180.0 ||
                        rotY > 180.0 ||
                        rotZ > 180.0)
                    {
                        anyInvalid = true;
                        Console.WriteLine($"Shape ID {shape.ID} on Page {page.Name} exceeds allowed rotation:");
                        Console.WriteLine($"  2D Angle: {angle2D}°");
                        Console.WriteLine($"  3D RotationX: {rotX}°");
                        Console.WriteLine($"  3D RotationY: {rotY}°");
                        Console.WriteLine($"  3D RotationZ: {rotZ}°");
                    }
                }
            }

            if (!anyInvalid)
            {
                Console.WriteLine("Validation passed: No shape exceeds 180 degrees rotation on any axis.");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}