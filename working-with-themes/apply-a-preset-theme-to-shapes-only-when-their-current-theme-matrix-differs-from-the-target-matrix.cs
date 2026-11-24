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

            // Define the target preset theme.
            PresetThemeValue targetTheme = PresetThemeValue.Bubble;
            PresetThemeVariantValue targetVariant = PresetThemeVariantValue.Variant1;
            // PresetQuickStyleValue targetQuickStyle = PresetQuickStyleValue.VariantStyle1; // optional

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // TODO: Determine the current theme matrix of the shape.
                    // The Aspose.Diagram API provides write‑only properties for PresetTheme,
                    // PresetThemeVariant and PresetThemeQuickStyle, so the current theme
                    // cannot be read directly. Implement custom logic here if a reliable
                    // method becomes available in a future API version.

                    bool needsThemeChange = true; // Placeholder: assume change is needed.

                    if (needsThemeChange)
                    {
                        // Apply the preset theme to the shape.
                        shape.PresetTheme = targetTheme;
                        shape.PresetThemeVariant = targetVariant;
                        // shape.PresetThemeQuickStyle = targetQuickStyle; // optional
                    }
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}