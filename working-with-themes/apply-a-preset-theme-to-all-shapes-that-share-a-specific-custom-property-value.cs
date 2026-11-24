using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input file, property name, property value, optional output file
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <customPropName> <customPropValue> [outputVisioPath]");
            return;
        }

        string inputPath = args[0];
        string targetPropName = args[1];
        string targetPropValue = args[2];
        string outputPath = args.Length >= 4 ? args[3] : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", Path.GetFileNameWithoutExtension(inputPath) + "_themed.vsdx");

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Check each custom property of the shape
                    foreach (Prop prop in shape.Props)
                    {
                        // Ensure the property has a value before comparing
                        if (prop.Name == targetPropName && prop.Value != null && prop.Value.Val == targetPropValue)
                        {
                            // Apply a preset theme to the matching shape
                            // Using the confirmed PresetThemeValue.Bubble enum member
                            shape.PresetTheme = PresetThemeValue.Bubble;

                            // Optionally, set a variant (write‑only) if desired
                            // shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;

                            // No need to continue checking other properties for this shape
                            break;
                        }
                    }
                }
            }

            // Save the modified diagram to the specified output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved with themed shapes to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}