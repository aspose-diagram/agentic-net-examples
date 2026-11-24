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
            // Load the Visio diagram inside a using block (required by the API).
            using (Diagram diagram = new Diagram(inputPath))
            {
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
                Shape shape = page.Shapes[0];

                // ------------------------------------------------------------
                // Apply a preset theme to the shape.
                // ------------------------------------------------------------
                shape.PresetTheme = PresetThemeValue.Bubble;
                shape.PresetThemeVariant = PresetThemeVariantValue.Variant3;
                shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle2;

                // ------------------------------------------------------------
                // Enhance visual depth by configuring a simple drop shadow.
                // ------------------------------------------------------------
                // Set shadow type to Simple.
                shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;
                // Set shadow color (black).
                shape.Fill.ShdwForegnd.Value = "#000000";
                // Set shadow transparency to 30% (0.3 = 30% transparent).
                shape.Fill.ShdwForegndTrans.Value = 0.3;
                // Set shadow offsets (in inches) for a subtle offset.
                shape.Fill.ShapeShdwOffsetX.Value = 0.1;
                shape.Fill.ShapeShdwOffsetY.Value = 0.1;

                // ------------------------------------------------------------
                // Save the modified diagram to the specified output path.
                // ------------------------------------------------------------
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