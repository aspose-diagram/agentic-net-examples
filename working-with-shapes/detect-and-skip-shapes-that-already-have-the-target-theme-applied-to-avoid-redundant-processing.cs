using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "Themes1.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "Themes1_Updated.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Define the target preset theme to apply
            PresetThemeValue targetTheme = PresetThemeValue.Bubble;

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Check if the shape already has the target theme applied
                    bool alreadyHasTheme = false;
                    foreach (Prop prop in shape.Props)
                    {
                        if (prop.Name == "AppliedTheme")
                        {
                            // Compare stored theme name with the target theme
                            if (prop.Value.Val == targetTheme.ToString())
                                alreadyHasTheme = true;
                            break;
                        }
                    }

                    // Skip processing if the theme is already applied
                    if (alreadyHasTheme)
                        continue;

                    // Apply the preset theme to the shape (write‑only property)
                    shape.PresetTheme = targetTheme;

                    // Record that the theme has been applied using a custom property
                    Prop themeProp = null;
                    foreach (Prop prop in shape.Props)
                    {
                        if (prop.Name == "AppliedTheme")
                        {
                            themeProp = prop;
                            break;
                        }
                    }

                    // If the custom property does not exist, create and add it
                    if (themeProp == null)
                    {
                        themeProp = new Prop();
                        shape.Props.Add(themeProp);
                    }

                    // Set property details
                    themeProp.Name = "AppliedTheme";
                    themeProp.Label.Value = "Applied Theme";
                    themeProp.Prompt.Value = "";
                    themeProp.Value.Val = targetTheme.ToString();
                }
            }

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}