using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Verify that the diagram contains at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Get the first page of the diagram
            Page page = diagram.Pages[0];

            // Verify that the page contains at least one shape
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The page contains no shapes.");
                return;
            }

            // Retrieve the first shape on the page
            Shape shape = page.Shapes[0];

            // ------------------------------------------------------------
            // Apply a preset theme and variant to the shape (write‑only properties)
            // ------------------------------------------------------------
            shape.PresetTheme = PresetThemeValue.Bubble;
            shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;

            // ------------------------------------------------------------
            // Apply 3‑D rotation to create a perspective view
            // ------------------------------------------------------------
            // Set the rotation type to Perspective
            shape.ThreeDFormat.RotationType.Value = RotationTypeValue.Perspective;

            // Set rotation angles (degrees) for X, Y, and Z axes
            shape.ThreeDFormat.RotationXAngle.Value = 30.0; // X‑axis rotation
            shape.ThreeDFormat.RotationYAngle.Value = 20.0; // Y‑axis rotation
            shape.ThreeDFormat.RotationZAngle.Value = 0.0;  // Z‑axis rotation

            // Keep text flat while rotating the shape
            shape.ThreeDFormat.KeepTextFlat.Value = BOOL.True;

            // ------------------------------------------------------------
            // Save the modified diagram to a new file
            // ------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}