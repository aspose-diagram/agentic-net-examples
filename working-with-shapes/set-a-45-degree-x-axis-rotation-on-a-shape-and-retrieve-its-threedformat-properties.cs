using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        // Output Visio file path after modification
        string outputPath = "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page
            Page page = diagram.Pages[0];

            // Find the first non-deleted shape on the page
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                // Skip shapes that are marked as deleted
                if (shape.Del == BOOL.True)
                    continue;

                targetShape = shape;
                break;
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine("No usable shape found on the first page.");
                return;
            }

            // Set a 45-degree rotation around the X‑axis using the ThreeDFormat
            targetShape.ThreeDFormat.RotationXAngle.Value = 45.0;

            // Retrieve and display various ThreeDFormat properties
            Console.WriteLine("ThreeDFormat properties of the modified shape:");
            Console.WriteLine($"RotationXAngle: {targetShape.ThreeDFormat.RotationXAngle.Value}");
            Console.WriteLine($"RotationYAngle: {targetShape.ThreeDFormat.RotationYAngle.Value}");
            Console.WriteLine($"RotationZAngle: {targetShape.ThreeDFormat.RotationZAngle.Value}");
            Console.WriteLine($"RotationType: {targetShape.ThreeDFormat.RotationType.Value}");
            Console.WriteLine($"Perspective: {targetShape.ThreeDFormat.Perspective.Value}");
            Console.WriteLine($"DistanceFromGround: {targetShape.ThreeDFormat.DistanceFromGround.Value}");
            Console.WriteLine($"KeepTextFlat: {targetShape.ThreeDFormat.KeepTextFlat.Value}");

            // Save the modified diagram to a new file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}