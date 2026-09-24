using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Ensure a file path argument is provided.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: <program> <VisioFilePath>");
            return;
        }

        // Assign the first argument to a variable.
        string visioPath = args[0];
        // Guard: verify the file exists.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(visioPath);

            bool violationFound = false;
            int pageIndex = 0; // Zero‑based page index for reporting.

            // Iterate through each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Check Z‑axis rotation (shape's own angle).
                    double angleZ = shape.XForm.Angle.Value;
                    if (Math.Abs(angleZ) > 180)
                    {
                        Console.WriteLine($"Violation: Page {pageIndex}, Shape ID {shape.ID}, Name '{shape.NameU}', Axis Z, Angle {angleZ}");
                        violationFound = true;
                    }

                    // Check 3‑D X‑axis rotation if present.
                    double angleX = shape.ThreeDFormat.RotationXAngle.Value;
                    if (Math.Abs(angleX) > 180)
                    {
                        Console.WriteLine($"Violation: Page {pageIndex}, Shape ID {shape.ID}, Name '{shape.NameU}', Axis X, Angle {angleX}");
                        violationFound = true;
                    }

                    // Check 3‑D Y‑axis rotation if present.
                    double angleY = shape.ThreeDFormat.RotationYAngle.Value;
                    if (Math.Abs(angleY) > 180)
                    {
                        Console.WriteLine($"Violation: Page {pageIndex}, Shape ID {shape.ID}, Name '{shape.NameU}', Axis Y, Angle {angleY}");
                        violationFound = true;
                    }

                    // Check 3‑D Z‑axis rotation if present.
                    double angleZ3D = shape.ThreeDFormat.RotationZAngle.Value;
                    if (Math.Abs(angleZ3D) > 180)
                    {
                        Console.WriteLine($"Violation: Page {pageIndex}, Shape ID {shape.ID}, Name '{shape.NameU}', Axis Z (3D), Angle {angleZ3D}");
                        violationFound = true;
                    }
                }

                pageIndex++; // Move to the next page index.
            }

            // Report overall result if no violations were detected.
            if (!violationFound)
            {
                Console.WriteLine("No shape exceeds a rotation angle of 180 degrees on any axis.");
            }
        }
        catch (Exception ex)
        {
            // Output any unexpected errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}