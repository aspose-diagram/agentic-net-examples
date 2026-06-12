using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the target preset theme values
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

                        // Determine if the shape already has the target theme applied.
                        // Since PresetTheme is write‑only, we use a custom user cell as a marker.
                        bool themeApplied = false;
                        foreach (User user in shape.Users)
                        {
                            if (user.Name == "ThemeApplied")
                            {
                                themeApplied = true;
                                break;
                            }
                        }

                        // Apply the theme only when it has not been applied yet
                        if (!themeApplied)
                        {
                            shape.PresetTheme = targetTheme;
                            shape.PresetThemeVariant = targetVariant;
                            shape.PresetThemeQuickStyle = targetQuickStyle;

                            // Add a marker user cell to indicate the theme has been set
                            User marker = new User();
                            marker.Name = "ThemeApplied";
                            marker.Value.Val = "true";
                            shape.Users.Add(marker);
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