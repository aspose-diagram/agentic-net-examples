using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Define the target theme identifiers
            const PresetThemeValue targetTheme = PresetThemeValue.Bubble;
            const PresetThemeVariantValue targetVariant = PresetThemeVariantValue.Variant1;
            const PresetQuickStyleValue targetQuickStyle = PresetQuickStyleValue.VariantStyle1;

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the Props collection is available
                    if (shape.Props == null)
                        continue;

                    // Look for a custom property that records the applied theme
                    Prop themeProp = null;
                    foreach (Prop p in shape.Props)
                    {
                        if (p.Name == "AppliedTheme")
                        {
                            themeProp = p;
                            break;
                        }
                    }

                    bool needsTheme = false;

                    if (themeProp == null)
                    {
                        // No record – theme has not been applied yet
                        needsTheme = true;
                    }
                    else
                    {
                        // Compare stored theme name with the target
                        if (!string.Equals(themeProp.Value.Val, targetTheme.ToString(), StringComparison.OrdinalIgnoreCase))
                        {
                            needsTheme = true;
                        }
                    }

                    if (needsTheme)
                    {
                        // Apply the preset theme to the shape
                        shape.PresetTheme = targetTheme;
                        shape.PresetThemeVariant = targetVariant;
                        shape.PresetThemeQuickStyle = targetQuickStyle;

                        // Record the applied theme in a custom property
                        if (themeProp == null)
                        {
                            themeProp = new Prop();
                            themeProp.Name = "AppliedTheme";
                            shape.Props.Add(themeProp);
                        }
                        themeProp.Value.Val = targetTheme.ToString();
                    }
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
