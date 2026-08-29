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
                string desiredThemeName = "Bubble";

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Check if the shape already has a custom property indicating the applied theme
                        bool themeMatches = false;
                        foreach (Prop prop in shape.Props)
                        {
                            if (prop.Name == "AppliedTheme")
                            {
                                if (prop.Value.Val == desiredThemeName)
                                    themeMatches = true;
                                break;
                            }
                        }

                        // Apply the theme only if it does not match the desired one
                        if (!themeMatches)
                        {
                            // Apply the preset theme to the shape
                            shape.PresetTheme = desiredTheme;

                            // Optionally set a variant (example Variant1)
                            shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;

                            // Record the applied theme in a custom property for future checks
                            Prop themeProp = new Prop();
                            themeProp.Name = "AppliedTheme";
                            themeProp.Value.Val = desiredThemeName;
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