using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Get the first shape on the active page (skip background shapes).
            Shape targetShape = null;
            foreach (Shape shape in diagram.ActivePage.Shapes)
            {
                // Ensure the shape is not a group placeholder and is not deleted.
                if (shape.Del == BOOL.False)
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine("No suitable shape found to rotate.");
                return;
            }

            // Apply 2D rotation (optional, demonstrates combined effect).
            targetShape.XForm.Angle.Value = 30.0;

            // Apply 3D rotations of 30 degrees on X, Y, and Z axes.
            targetShape.ThreeDFormat.RotationXAngle.Value = 30.0;
            targetShape.ThreeDFormat.RotationYAngle.Value = 30.0;
            targetShape.ThreeDFormat.RotationZAngle.Value = 30.0;

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}