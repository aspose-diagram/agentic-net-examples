using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the first argument to be the path to the Visio file.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string diagramPath = args[0];

        // Guard: verify the file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            bool validationFailed = false;

            // Iterate through each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Check 2D rotation angle (shape.XForm.Angle).
                    double angle2D = shape.XForm.Angle.Value;
                    if (Math.Abs(angle2D) > 180.0)
                    {
                        Console.Error.WriteLine($"Shape ID {shape.ID} on page {page.Name} exceeds 2D rotation limit: {angle2D} degrees.");
                        validationFailed = true;
                    }

                    // Check 3D rotation angles if the ThreeDFormat is available.
                    if (shape.ThreeDFormat != null)
                    {
                        double rotX = shape.ThreeDFormat.RotationXAngle.Value;
                        double rotY = shape.ThreeDFormat.RotationYAngle.Value;
                        double rotZ = shape.ThreeDFormat.RotationZAngle.Value;

                        if (Math.Abs(rotX) > 180.0)
                        {
                            Console.Error.WriteLine($"Shape ID {shape.ID} on page {page.Name} exceeds X-axis rotation limit: {rotX} degrees.");
                            validationFailed = true;
                        }
                        if (Math.Abs(rotY) > 180.0)
                        {
                            Console.Error.WriteLine($"Shape ID {shape.ID} on page {page.Name} exceeds Y-axis rotation limit: {rotY} degrees.");
                            validationFailed = true;
                        }
                        if (Math.Abs(rotZ) > 180.0)
                        {
                            Console.Error.WriteLine($"Shape ID {shape.ID} on page {page.Name} exceeds Z-axis rotation limit: {rotZ} degrees.");
                            validationFailed = true;
                        }
                    }
                }
            }

            if (!validationFailed)
            {
                Console.WriteLine("Validation passed: No shape exceeds a rotation angle of 180 degrees on any axis.");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose or I/O errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}