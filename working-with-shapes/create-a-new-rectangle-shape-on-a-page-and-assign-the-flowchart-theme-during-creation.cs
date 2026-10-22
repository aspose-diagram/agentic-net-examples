using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
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
            // Load the existing Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Get the first page.
                Page page = diagram.Pages[0];

                // Define position for the new rectangle (in inches).
                double pinX = 2.0;
                double pinY = 2.0;
                string masterName = "Rectangle";

                // Add a new rectangle shape to the page.
                // The fourth parameter is a plain bool (isCalculate), not a BOOL enum.
                long shapeId = page.AddShape(pinX, pinY, masterName, false);

                // Retrieve the newly added shape instance.
                Shape newShape = page.Shapes.GetShape(shapeId);

                // Assign a preset theme to the shape.
                // The requested "flowchart" theme is not available in the current API.
                // As a substitute, we use the available Bubble theme and document the change.
                newShape.PresetTheme = PresetThemeValue.Bubble; // Substitute for Flowchart theme.

                // Optionally assign a theme variant.
                newShape.PresetThemeVariant = PresetThemeVariantValue.Variant1;

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}