using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path – change as needed.
        string inputPath = "input.vsdx";
        // Guard to ensure the file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path where the modified diagram will be saved.
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram contains at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page.
            Page page = diagram.Pages[0];

            // Ensure the page contains at least one shape.
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Retrieve the first shape on the page.
            Shape shape = page.Shapes.GetShape(0);

            // Configure the shape's 3‑D rotation to Perspective.
            shape.ThreeDFormat.RotationType.Value = RotationTypeValue.Perspective;

            // Assign a 45‑degree perspective angle.
            shape.ThreeDFormat.Perspective.Value = 45.0;

            // Save the modified diagram to the output file in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}