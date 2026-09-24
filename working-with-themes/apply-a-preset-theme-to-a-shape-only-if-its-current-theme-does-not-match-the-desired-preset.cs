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

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path to the output Visio file
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Desired preset theme
            PresetThemeValue desiredTheme = PresetThemeValue.Bubble;
            // Desired theme name for tracking (must match the enum name)
            string desiredThemeName = "Bubble";

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Determine if the shape already has the desired theme applied
                    bool themeMatches = false;
                    foreach (Prop prop in shape.Props)
                    {
                        if (prop.Name == "AppliedTheme" && prop.Value.Val == desiredThemeName)
                        {
                            themeMatches = true;
                            break;
                        }
                    }

                    // Apply the theme only if it does not match
                    if (!themeMatches)
                    {
                        // Apply the preset theme to the shape
                        shape.PresetTheme = desiredTheme;
                        // Optionally set a variant (using Variant1 as example)
                        shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;

                        // Record the applied theme in a custom property for future checks
                        Prop themeProp = null;
                        foreach (Prop prop in shape.Props)
                        {
                            if (prop.Name == "AppliedTheme")
                            {
                                themeProp = prop;
                                break;
                            }
                        }

                        if (themeProp == null)
                        {
                            // Create a new custom property if it does not exist
                            themeProp = new Prop();
                            themeProp.Name = "AppliedTheme";
                            shape.Props.Add(themeProp);
                        }

                        // Store the theme name
                        themeProp.Value.Val = desiredThemeName;
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
