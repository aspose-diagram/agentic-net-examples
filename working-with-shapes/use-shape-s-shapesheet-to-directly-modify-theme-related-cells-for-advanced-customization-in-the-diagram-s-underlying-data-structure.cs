using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Verify that the diagram contains at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Access the first page
                Page firstPage = diagram.Pages[0];

                // Verify that the page contains at least one shape
                if (firstPage.Shapes.Count == 0)
                {
                    Console.Error.WriteLine("The first page contains no shapes.");
                    return;
                }

                // Retrieve the first shape on the first page
                Shape shape = firstPage.Shapes[0];

                // ------------------------------------------------------------
                // Directly modify theme‑related cells via the ShapeSheet.
                // These assignments override theme defaults for this shape.
                // ------------------------------------------------------------

                // Apply a preset theme to the shape (write‑only property)
                shape.PresetTheme = PresetThemeValue.Bubble;

                // Apply a specific variant of the preset theme
                shape.PresetThemeVariant = PresetThemeVariantValue.Variant3;

                // Apply a quick‑style variant of the preset theme
                shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle2;

                // Further customize the theme by setting style and color matrices
                shape.SetPresetThemeStyleMatrics(PresetStyleMatricsValue.Style2, PresetColorMatricsValue.Color7);

                // Adjust fill foreground transparency (0.0 = opaque, 1.0 = fully transparent)
                shape.Fill.FillForegndTrans.Value = 0.20; // 20 % transparent (80 % opaque)

                // Override the line color with a theme‑based blue (hex string)
                shape.Line.LineColor.Value = "#0000FF";

                // Increase line weight for visual emphasis (in inches)
                shape.Line.LineWeight.Value = 0.03;

                // ------------------------------------------------------------
                // Save the modified diagram to the specified output path.
                // ------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}