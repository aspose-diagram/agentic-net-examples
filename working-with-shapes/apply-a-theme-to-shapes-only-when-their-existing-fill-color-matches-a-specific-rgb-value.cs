using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> <targetFillColorHex>");
            Console.Error.WriteLine("Example: Program diagram.vsdx diagram_modified.vsdx #FF0000");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string targetColor = args[2];

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

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                // Iterate using the shape collection count.
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    // Retrieve the shape ID via the collection.
                    long shapeId = page.Shapes[i].ID;

                    // Get the actual shape instance.
                    Shape shape = page.Shapes.GetShape(shapeId);
                    if (shape == null)
                        continue; // Safety check.

                    // Obtain the foreground fill color string (e.g., "#FF0000").
                    string fillColor = shape.Fill?.FillForegnd?.Value;

                    // Compare with the target color (case‑insensitive).
                    if (!string.IsNullOrEmpty(fillColor) &&
                        fillColor.Equals(targetColor, StringComparison.OrdinalIgnoreCase))
                    {
                        // Apply a preset theme to the matching shape.
                        // Using Bubble theme as an example.
                        shape.PresetTheme = PresetThemeValue.Bubble;
                        shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                        shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;

                        Console.WriteLine($"Applied theme to shape ID {shapeId} on page {page.ID} (fill {fillColor}).");
                    }
                }
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine("An error occurred while processing the diagram:");
            Console.Error.WriteLine(ex.Message);
        }
    }
}