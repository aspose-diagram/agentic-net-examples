using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path for the modified Visio file
                string outputPath = "output.vsdx";

                // Load the diagram from file
                Diagram diagram = new Diagram(inputPath);

                // Define the custom property name and the value to match
                const string targetPropName = "Category";
                const string targetPropValue = "Important";

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has custom properties
                        if (shape.Props == null) continue;

                        // Look for the target custom property
                        foreach (Prop prop in shape.Props)
                        {
                            if (prop.Name == targetPropName && prop.Value.Val == targetPropValue)
                            {
                                // Apply a preset theme to the matching shape
                                shape.PresetTheme = PresetThemeValue.Bubble;
                                shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                                shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;
                                // No need to continue checking other properties for this shape
                                break;
                            }
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