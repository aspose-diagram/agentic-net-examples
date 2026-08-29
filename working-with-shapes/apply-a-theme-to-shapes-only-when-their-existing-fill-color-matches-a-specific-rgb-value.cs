using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output Visio file path
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the target fill color (hex string, case‑insensitive)
                const string targetFillColor = "#FF0000"; // Red

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the Fill and FillForegnd cells exist
                        if (shape.Fill == null || shape.Fill.FillForegnd == null)
                            continue;

                        string currentFill = shape.Fill.FillForegnd.Value;
                        if (string.IsNullOrEmpty(currentFill))
                            continue;

                        // Apply theme only when the fill color matches the target color
                        if (string.Equals(currentFill, targetFillColor, StringComparison.OrdinalIgnoreCase))
                        {
                            // Apply a preset theme to the shape
                            shape.PresetTheme = PresetThemeValue.Bubble;
                            shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }