using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

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

                        // Retrieve the Data1 field (string)
                        string dataValue = shape.Data1 ?? string.Empty;

                        // Conditional theme selection based on Data1 value
                        if (dataValue.Equals("Red", StringComparison.OrdinalIgnoreCase))
                        {
                            // Apply Bubble theme with Variant1 and QuickStyle1
                            shape.PresetTheme = PresetThemeValue.Bubble;
                            shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;
                        }
                        else if (dataValue.Equals("Blue", StringComparison.OrdinalIgnoreCase))
                        {
                            // Apply Bubble theme with Variant2 and QuickStyle2
                            shape.PresetTheme = PresetThemeValue.Bubble;
                            shape.PresetThemeVariant = PresetThemeVariantValue.Variant2;
                            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle2;
                        }
                        else if (dataValue.Equals("Green", StringComparison.OrdinalIgnoreCase))
                        {
                            // Apply Bubble theme with Variant3 and QuickStyle3
                            shape.PresetTheme = PresetThemeValue.Bubble;
                            shape.PresetThemeVariant = PresetThemeVariantValue.Variant3;
                            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle3;
                        }
                        else
                        {
                            // Default theme for shapes without matching Data1
                            shape.PresetTheme = PresetThemeValue.Bubble;
                            shape.PresetThemeVariant = PresetThemeVariantValue.Variant4;
                            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle4;
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