using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Paths for input and output diagrams
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Desired preset theme and variant
            PresetThemeValue desiredTheme = PresetThemeValue.Bubble;
            PresetThemeVariantValue desiredVariant = PresetThemeVariantValue.Variant1;

            // Name of the custom property used to track applied theme
            const string themePropName = "AppliedTheme";

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Determine if the shape already has the desired theme applied
                    bool themeAlreadyApplied = false;
                    if (shape.Props != null)
                    {
                        foreach (Prop prop in shape.Props)
                        {
                            if (prop.Name == themePropName && prop.Value.Val == desiredTheme.ToString())
                            {
                                themeAlreadyApplied = true;
                                break;
                            }
                        }
                    }

                    // Apply the theme only if it is not already applied
                    if (!themeAlreadyApplied)
                    {
                        shape.PresetTheme = desiredTheme;
                        shape.PresetThemeVariant = desiredVariant;

                        // Record the applied theme in a custom property
                        Prop themeProp = new Prop();
                        themeProp.Name = themePropName;
                        themeProp.Value.Val = desiredTheme.ToString();
                        shape.Props.Add(themeProp);
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
