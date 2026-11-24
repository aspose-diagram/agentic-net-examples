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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the custom data field (Data1) that determines the theme.
                        // Data1 is a simple string property; trim whitespace for safety.
                        string themeKey = shape.Data1?.Trim();

                        // Conditional theme selection based on the value of Data1.
                        // Example: apply the "Bubble" preset theme when Data1 equals "Bubble".
                        if (string.Equals(themeKey, "Bubble", StringComparison.OrdinalIgnoreCase))
                        {
                            // Apply the Bubble preset theme to the shape.
                            shape.PresetTheme = PresetThemeValue.Bubble;
                            // Optionally set a variant (using Variant1 as an example).
                            shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                        }
                        // Additional theme keys can be handled here, e.g.:
                        // else if (string.Equals(themeKey, "Clouds", StringComparison.OrdinalIgnoreCase))
                        // {
                        //     shape.PresetTheme = PresetThemeValue.Clouds;
                        //     shape.PresetThemeVariant = PresetThemeVariantValue.Variant2;
                        // }
                        // If the key does not match any known theme, no theme is applied (default appearance).
                    }
                }

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}