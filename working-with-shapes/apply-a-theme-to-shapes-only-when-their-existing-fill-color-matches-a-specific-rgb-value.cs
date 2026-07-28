using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output Visio file path
                string outputPath = "output.vsdx";

                // The fill color to match (hex string, case-insensitive)
                const string targetFillColor = "#FF0000"; // Red

                // Load the diagram
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

                        // Get the foreground fill color (hex string). May be null.
                        string fillColor = shape.Fill?.FillForegnd?.Value;

                        // Compare ignoring case
                        if (!string.IsNullOrEmpty(fillColor) && 
                            string.Equals(fillColor, targetFillColor, StringComparison.OrdinalIgnoreCase))
                        {
                            // Apply a preset theme to the shape
                            shape.PresetTheme = PresetThemeValue.Bubble;
                            shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                            // Optionally set a quick style
                            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle2;
                        }
                    }
                }

                // Save the modified diagram in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }