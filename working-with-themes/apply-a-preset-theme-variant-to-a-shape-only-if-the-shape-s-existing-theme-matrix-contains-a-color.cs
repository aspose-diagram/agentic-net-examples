using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for SaveFileFormat enum

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths from command‑line arguments or defaults
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the source Visio file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through every page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through every shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Determine whether the shape already has a fill color defined
                    bool hasColor = false;

                    // Check inherited fill color (theme matrix) – non‑empty string indicates a color
                    if (shape.InheritFill != null && !string.IsNullOrWhiteSpace(shape.InheritFill.FillForegnd.Value))
                    {
                        hasColor = true;
                    }
                    // Fallback: check the shape's own fill color if inheritance is not set
                    else if (shape.Fill != null && !string.IsNullOrWhiteSpace(shape.Fill.FillForegnd.Value))
                    {
                        hasColor = true;
                    }

                    // Apply a preset theme variant only when a color is present
                    if (hasColor)
                    {
                        // Set the preset theme (write‑only property)
                        shape.PresetTheme = PresetThemeValue.Bubble;
                        // Set the preset theme variant (write‑only property)
                        shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                    }
                }
            }

            // Save the modified diagram to the output path using VSDX format
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