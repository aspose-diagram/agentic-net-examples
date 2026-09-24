using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Define the preset theme to apply
                    PresetThemeValue presetTheme = PresetThemeValue.Bubble;
                    PresetThemeVariantValue presetVariant = PresetThemeVariantValue.Variant1;
                    PresetQuickStyleValue quickStyle = PresetQuickStyleValue.VariantStyle1;

                    // Iterate through each page and apply the theme
                    foreach (Page page in diagram.Pages)
                    {
                        // Apply theme to the page
                        page.PresetTheme = presetTheme;
                        page.PresetThemeVariant = presetVariant;

                        // Log the page theme application
                        Console.WriteLine($"Applied preset theme '{presetTheme}' (variant '{presetVariant}') to page '{page.Name}'.");

                        // Iterate through each shape on the page and apply the same theme
                        foreach (Shape shape in page.Shapes)
                        {
                            // Apply theme to the shape
                            shape.PresetTheme = presetTheme;
                            shape.PresetThemeVariant = presetVariant;
                            shape.PresetThemeQuickStyle = quickStyle;

                            // Log the shape theme application
                            Console.WriteLine($"Applied preset theme '{presetTheme}' (variant '{presetVariant}', quick style '{quickStyle}') to shape ID {shape.ID} on page '{page.Name}'.");
                        }
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved to '{outputPath}'.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }