using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for Diagram.Save overloads

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path – change as needed
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path – change as needed
        string outputPath = "output.vsdx";

        // Custom property name and value that identify target shapes
        string targetPropName = "Category";
        string targetPropValue = "Important";

        try
        {
            // Load the diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a Props collection before accessing it
                    if (shape.Props == null) continue;

                    // Flag indicating whether the shape matches the custom property criteria
                    bool matches = false;

                    // Search for the custom property by name and compare its value
                    foreach (Prop prop in shape.Props)
                    {
                        if (prop.Name == targetPropName && prop.Value.Val == targetPropValue)
                        {
                            matches = true;
                            break;
                        }
                    }

                    // If the shape matches, apply the preset theme settings
                    if (matches)
                    {
                        // Apply a preset theme (write‑only property)
                        shape.PresetTheme = PresetThemeValue.Bubble;

                        // Apply a theme variant (write‑only property)
                        shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;

                        // Apply a quick style variant (write‑only property)
                        shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;
                    }
                }
            }

            // Save the modified diagram using the VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}