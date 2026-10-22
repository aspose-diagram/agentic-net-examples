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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page.
            Page page = diagram.Pages[0];

            // Ensure the page has at least one shape.
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Retrieve the first shape on the page.
            Shape shape = page.Shapes[0];

            // ------------------------------------------------------------
            // Apply a preset theme to the shape.
            // ------------------------------------------------------------
            // PresetTheme, PresetThemeVariant and PresetThemeQuickStyle are write‑only.
            shape.PresetTheme = PresetThemeValue.Bubble;
            shape.PresetThemeVariant = PresetThemeVariantValue.Variant3;
            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle2;

            // ------------------------------------------------------------
            // Apply 3‑D rotation to create a perspective view.
            // ------------------------------------------------------------
            // Set rotation angles (in degrees) for X, Y and Z axes.
            shape.ThreeDFormat.RotationXAngle.Value = 30.0; // X‑axis rotation
            shape.ThreeDFormat.RotationYAngle.Value = 20.0; // Y‑axis rotation
            shape.ThreeDFormat.RotationZAngle.Value = 10.0; // Z‑axis rotation

            // Choose a rotation type that accounts for perspective.
            shape.ThreeDFormat.RotationType.Value = RotationTypeValue.Perspective;

            // Optional: keep text flat while rotating the shape.
            shape.ThreeDFormat.KeepTextFlat.Value = BOOL.True;

            // Optional: adjust perspective depth (0 = no perspective, higher = stronger).
            shape.ThreeDFormat.Perspective.Value = 0.5; // example value

            // Optional: distance from ground (affects visual depth).
            shape.ThreeDFormat.DistanceFromGround.Value = 0.2; // example value

            // ------------------------------------------------------------
            // Save the modified diagram.
            // ------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}