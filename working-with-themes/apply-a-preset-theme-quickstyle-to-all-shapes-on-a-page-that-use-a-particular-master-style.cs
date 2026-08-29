using System.IO;
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

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Define the master name to filter shapes
                string targetMasterName = "Rectangle"; // change as needed

                // Define the theme, variant, and quickstyle to apply
                PresetThemeValue theme = PresetThemeValue.Bubble;
                PresetThemeVariantValue variant = PresetThemeVariantValue.Variant1;
                PresetQuickStyleValue quickStyle = PresetQuickStyleValue.VariantStyle2;

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has an associated master
                        if (shape.Master != null && shape.Master.Name == targetMasterName)
                        {
                            // Apply the preset theme, variant, and quickstyle
                            shape.PresetTheme = theme;
                            shape.PresetThemeVariant = variant;
                            shape.PresetThemeQuickStyle = quickStyle;
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Theme quickstyle applied and diagram saved to: " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
