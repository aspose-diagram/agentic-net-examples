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
            Console.Error.WriteLine("Usage: Program <inputFilePath> <outputFilePath>");
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

            // Ensure the diagram contains at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page.
            Page page = diagram.Pages[0];

            // Find the first non‑deleted shape on the page.
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                // Skip logically deleted shapes.
                if (shape.Del == BOOL.False)
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine("No suitable shape found on the first page.");
                return;
            }

            // Set a 45‑degree rotation around the X‑axis.
            targetShape.ThreeDFormat.RotationXAngle.Value = 45.0;

            // Retrieve and display ThreeDFormat properties.
            Console.WriteLine("ThreeDFormat properties of the selected shape:");
            Console.WriteLine($"RotationXAngle: {targetShape.ThreeDFormat.RotationXAngle.Value}");
            Console.WriteLine($"RotationYAngle: {targetShape.ThreeDFormat.RotationYAngle.Value}");
            Console.WriteLine($"RotationZAngle: {targetShape.ThreeDFormat.RotationZAngle.Value}");
            Console.WriteLine($"RotationType: {targetShape.ThreeDFormat.RotationType.Value}");
            Console.WriteLine($"Perspective: {targetShape.ThreeDFormat.Perspective.Value}");
            Console.WriteLine($"DistanceFromGround: {targetShape.ThreeDFormat.DistanceFromGround.Value}");
            Console.WriteLine($"KeepTextFlat: {targetShape.ThreeDFormat.KeepTextFlat.Value}");

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}