using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (modify as needed)
        string inputPath = "input.vsdx";
        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path
        string outputPath = "output_rotated.vsdx";

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Use the first page for shape operations
            Page page = diagram.Pages[0];

            // Find the first non‑connector shape (OneD == false)
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (!shape.OneD) // Exclude connector lines
                {
                    targetShape = shape;
                    break;
                }
            }

            // If no suitable shape is found, report and exit
            if (targetShape == null)
            {
                Console.Error.WriteLine("No suitable shape found to apply rotation.");
                return;
            }

            // Apply combined X, Y, and Z rotations of 30 degrees each
            // RotationXAngle, RotationYAngle, and RotationZAngle are in degrees
            targetShape.ThreeDFormat.RotationXAngle.Value = 30.0;
            targetShape.ThreeDFormat.RotationYAngle.Value = 30.0;
            targetShape.ThreeDFormat.RotationZAngle.Value = 30.0;

            // Save the modified diagram to the output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Inform the user of successful completion
            Console.WriteLine($"Shape rotated and diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}