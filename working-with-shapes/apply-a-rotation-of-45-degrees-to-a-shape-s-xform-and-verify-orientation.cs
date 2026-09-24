using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Ensure an input file path is provided.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        // Assign the input file path from the first argument.
        string inputPath = args[0];
        // Verify that the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Optional: define an output path for the modified diagram.
        string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "RotatedShape.vsdx");

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Access the first page of the diagram.
            Page page = diagram.Pages[0];

            // Retrieve the first shape on the page (skip any hidden/deleted shapes).
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                // Ensure the shape is not marked as deleted.
                if (shape.Del == BOOL.False)
                {
                    targetShape = shape;
                    break;
                }
            }

            // If no suitable shape was found, report and exit.
            if (targetShape == null)
            {
                Console.Error.WriteLine("No non-deleted shape found on the first page.");
                return;
            }

            // Apply a rotation of 45 degrees to the shape's XForm.
            targetShape.XForm.Angle.Value = 45.0;

            // Verify that the rotation was applied correctly.
            double appliedAngle = targetShape.XForm.Angle.Value;
            if (Math.Abs(appliedAngle - 45.0) < 0.001)
            {
                Console.WriteLine("Rotation applied successfully: 45 degrees.");
            }
            else
            {
                Console.Error.WriteLine($"Rotation verification failed. Expected 45, got {appliedAngle}.");
                // Optionally throw to indicate failure.
                throw new Exception("Rotation verification failed.");
            }

            // Save the modified diagram to a new file.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Modified diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}