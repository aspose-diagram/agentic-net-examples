using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the first page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }
            Page page = diagram.Pages[0];

            // Retrieve the first shape on the page.
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The page contains no shapes.");
                return;
            }
            Shape shape = page.Shapes[0];

            // Apply the preset theme "Bubble" to the shape.
            shape.PresetTheme = PresetThemeValue.Bubble;

            // Optionally set a theme variant (write‑only as well).
            shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;

            // Save the modified diagram to the output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Confirmation message.
            Console.WriteLine($"Preset theme 'Bubble' applied to shape ID {shape.ID} and diagram saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}