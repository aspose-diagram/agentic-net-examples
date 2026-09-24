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

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path for the processed output file
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the target theme to apply
                PresetThemeValue targetTheme = PresetThemeValue.Bubble;
                PresetThemeVariantValue targetVariant = PresetThemeVariantValue.Variant1;
                PresetQuickStyleValue targetQuickStyle = PresetQuickStyleValue.VariantStyle1;

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Check if the shape already has the theme applied.
                        // Since the PresetTheme property is write‑only, we use a custom
                        // shape property ("ThemeApplied") as a marker.
                        bool alreadyThemed = false;
                        if (shape.Props != null)
                        {
                            foreach (Prop prop in shape.Props)
                            {
                                if (prop.Name == "ThemeApplied" && prop.Value.Val == "True")
                                {
                                    alreadyThemed = true;
                                    break;
                                }
                            }
                        }

                        if (alreadyThemed)
                        {
                            // Shape already processed – skip further work
                            continue;
                        }

                        // Apply the desired theme to the shape
                        shape.PresetTheme = targetTheme;
                        shape.PresetThemeVariant = targetVariant;
                        shape.PresetThemeQuickStyle = targetQuickStyle;

                        // Mark the shape as themed to avoid future redundant processing
                        Prop themeMarker = new Prop();
                        themeMarker.Name = "ThemeApplied";
                        themeMarker.Value.Val = "True";
                        shape.Props.Add(themeMarker);
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