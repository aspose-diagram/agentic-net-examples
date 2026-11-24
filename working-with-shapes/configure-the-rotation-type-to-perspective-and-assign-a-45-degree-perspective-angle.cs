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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
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

        // Load the Visio diagram inside a using block as required.
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the first page (index 0). Adjust if a specific page is needed.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                Page page = diagram.Pages[0];

                // Attempt to retrieve a shape with ID 1. If it does not exist, report and exit.
                Shape shape = page.Shapes.GetShape(1);
                if (shape == null)
                {
                    Console.Error.WriteLine("Shape with ID 1 not found on the first page.");
                    return;
                }

                // Configure 3‑D rotation: set rotation type to Perspective.
                shape.ThreeDFormat.RotationType.Value = RotationTypeValue.Perspective;

                // Assign a 45‑degree perspective angle.
                shape.ThreeDFormat.Perspective.Value = 45;

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}