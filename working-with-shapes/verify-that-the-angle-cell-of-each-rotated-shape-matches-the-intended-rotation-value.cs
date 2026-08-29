using System.IO;
using System;
using Aspose.Diagram;

class VerifyShapeAngles
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Current rotation angle of the shape (radians)
                    double currentAngle = shape.XForm.Angle.Value;

                    // Consider a shape rotated if its angle is not effectively zero
                    if (Math.Abs(currentAngle) > 1e-6)
                    {
                        // Intended rotation value – using the 3‑D Z‑axis rotation as the reference
                        double intendedAngle = shape.ThreeDFormat?.RotationZAngle?.Value ?? 0.0;

                        // Verify that the angles match within a small tolerance
                        bool isMatch = Math.Abs(currentAngle - intendedAngle) < 1e-6;

                        Console.WriteLine(
                            $"Shape ID {shape.ID} on Page {page.ID}: " +
                            $"CurrentAngle={currentAngle} rad, IntendedAngle={intendedAngle} rad, Match={isMatch}");
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
