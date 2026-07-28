using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths for input and output diagrams
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Define the custom property name and the value to match
            string targetPropName = "Category";
            string targetPropValue = "Important";

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has custom properties
                    if (shape.Props != null)
                    {
                        foreach (Prop prop in shape.Props)
                        {
                            // Check for the specific custom property name and value
                            if (prop.Name == targetPropName && prop.Value.Val == targetPropValue)
                            {
                                // Apply a preset theme to the matching shape
                                shape.PresetTheme = PresetThemeValue.Bubble;
                                shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                                shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;
                                break; // Property matched; no need to check further properties
                            }
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
