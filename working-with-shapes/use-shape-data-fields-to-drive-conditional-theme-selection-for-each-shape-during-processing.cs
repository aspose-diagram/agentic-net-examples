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

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Use Shape Data fields (Data1, Data2, Data3) to decide the theme
                        // Example logic:
                        // - If Data1 equals "BlueTheme", apply Bubble theme with Variant1 and QuickStyle1
                        // - If Data2 equals "RedTheme", apply Bubble theme with Variant2 and QuickStyle2
                        // - Otherwise, apply Bubble theme with Variant3 and QuickStyle3

                        if (!string.IsNullOrEmpty(shape.Data1) && shape.Data1.Equals("BlueTheme", StringComparison.OrdinalIgnoreCase))
                        {
                            // Apply first theme configuration
                            shape.PresetTheme = PresetThemeValue.Bubble;
                            shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;
                        }
                        else if (!string.IsNullOrEmpty(shape.Data2) && shape.Data2.Equals("RedTheme", StringComparison.OrdinalIgnoreCase))
                        {
                            // Apply second theme configuration
                            shape.PresetTheme = PresetThemeValue.Bubble;
                            shape.PresetThemeVariant = PresetThemeVariantValue.Variant2;
                            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle2;
                        }
                        else
                        {
                            // Default theme configuration
                            shape.PresetTheme = PresetThemeValue.Bubble;
                            shape.PresetThemeVariant = PresetThemeVariantValue.Variant3;
                            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle3;
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