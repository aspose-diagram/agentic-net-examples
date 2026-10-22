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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> <customPropertyName> <customPropertyValue>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string targetPropName = args[2];
        string targetPropValue = args.Length > 3 ? args[3] : string.Empty;

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Check each custom property of the shape.
                    foreach (Prop prop in shape.Props)
                    {
                        // Ensure the property has a value before comparing.
                        if (prop.Name == targetPropName && prop.Value != null && prop.Value.Val == targetPropValue)
                        {
                            // Apply a preset theme to the matching shape.
                            shape.PresetTheme = PresetThemeValue.Bubble;
                            // Optionally set a variant (write‑only property).
                            shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                            // No need to continue checking other properties for this shape.
                            break;
                        }
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
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}