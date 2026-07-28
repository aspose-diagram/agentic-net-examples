using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Input and output file paths (adjust as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Define the target theme components
            PresetThemeValue targetTheme = PresetThemeValue.Bubble;
            PresetThemeVariantValue targetVariant = PresetThemeVariantValue.Variant1;
            PresetQuickStyleValue targetQuickStyle = PresetQuickStyleValue.VariantStyle1;

            // Create a string identifier to store in a custom property
            string themeIdentifier = $"{targetTheme}_{targetVariant}_{targetQuickStyle}";

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the Props collection is available
                    if (shape.Props == null)
                        continue;

                    // Check if the shape already has the target theme applied
                    bool alreadyApplied = false;
                    foreach (Prop prop in shape.Props)
                    {
                        if (prop.Name == "ThemeApplied" && prop.Value != null && prop.Value.Val == themeIdentifier)
                        {
                            alreadyApplied = true;
                            break;
                        }
                    }

                    // Skip processing if the theme is already applied
                    if (alreadyApplied)
                        continue;

                    // Apply the target theme to the shape
                    shape.PresetTheme = targetTheme;
                    shape.PresetThemeVariant = targetVariant;
                    shape.PresetThemeQuickStyle = targetQuickStyle;

                    // Record the applied theme in a custom property for future runs
                    Prop themeProp = null;
                    foreach (Prop prop in shape.Props)
                    {
                        if (prop.Name == "ThemeApplied")
                        {
                            themeProp = prop;
                            break;
                        }
                    }

                    if (themeProp == null)
                    {
                        themeProp = new Prop();
                        themeProp.Name = "ThemeApplied";
                        shape.Props.Add(themeProp);
                    }

                    themeProp.Value.Val = themeIdentifier;
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
