using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Define the target theme and variant
                PresetThemeValue targetTheme = PresetThemeValue.Bubble;
                PresetThemeVariantValue targetVariant = PresetThemeVariantValue.Variant1;

                // Name of the custom property used to track theme application
                const string themePropName = "ThemeApplied";

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Check if the shape already has the target theme applied
                        bool alreadyApplied = false;
                        foreach (Prop prop in shape.Props)
                        {
                            if (prop.Name == themePropName && prop.Value.Val == targetTheme.ToString())
                            {
                                alreadyApplied = true;
                                break;
                            }
                        }

                        if (alreadyApplied)
                        {
                            // Skip processing for this shape
                            Console.WriteLine($"Shape ID {shape.ID} already has theme {targetTheme}, skipping.");
                            continue;
                        }

                        // Apply the theme to the shape
                        shape.PresetTheme = targetTheme;
                        shape.PresetThemeVariant = targetVariant;

                        // Record the applied theme in a custom property
                        Prop themeProp = new Prop();
                        themeProp.Name = themePropName;
                        themeProp.Value.Val = targetTheme.ToString();
                        shape.Props.Add(themeProp);

                        Console.WriteLine($"Applied theme {targetTheme} to shape ID {shape.ID}.");
                    }
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine("Diagram saved to " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }