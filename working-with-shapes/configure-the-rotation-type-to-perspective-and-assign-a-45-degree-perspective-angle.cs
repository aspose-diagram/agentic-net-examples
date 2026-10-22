using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
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
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the first page (index 0). Ensure the diagram has at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                Page page = diagram.Pages[0];

                // Retrieve a shape to modify. Here we use the shape with ID 1 if it exists.
                // Adjust the ID as needed for your specific diagram.
                Shape shape = page.Shapes.GetShape(1);
                if (shape == null)
                {
                    Console.Error.WriteLine("Shape with ID 1 not found on the first page.");
                    return;
                }

                // Configure 3D rotation: set rotation type to Perspective.
                shape.ThreeDFormat.RotationType.Value = RotationTypeValue.Perspective;

                // Assign a 45‑degree perspective angle.
                shape.ThreeDFormat.Perspective.Value = 45;

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Diagram saved successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}