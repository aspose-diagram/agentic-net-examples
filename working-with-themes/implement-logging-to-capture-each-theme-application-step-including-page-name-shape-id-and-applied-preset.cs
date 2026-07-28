using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Define the preset theme and variant to apply
                    PresetThemeValue presetTheme = PresetThemeValue.Bubble;
                    PresetThemeVariantValue presetVariant = PresetThemeVariantValue.Variant1;
                    PresetQuickStyleValue quickStyle = PresetQuickStyleValue.VariantStyle1;

                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Apply the preset theme to the page
                        page.PresetTheme = presetTheme;
                        page.PresetThemeVariant = presetVariant;

                        // Log the page theme application
                        Console.WriteLine($"Page '{page.Name}' (ID: {page.ID}) - Applied PresetTheme: {presetTheme}, Variant: {presetVariant}");

                        // Iterate through each shape on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Apply the preset theme to the shape
                            shape.PresetTheme = presetTheme;
                            shape.PresetThemeVariant = presetVariant;
                            shape.PresetThemeQuickStyle = quickStyle;

                            // Log the shape theme application
                            long shapeId = shape.ID;
                            Console.WriteLine($"    Shape ID {shapeId} on page '{page.Name}' - Applied PresetTheme: {presetTheme}, Variant: {presetVariant}, QuickStyle: {quickStyle}");
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