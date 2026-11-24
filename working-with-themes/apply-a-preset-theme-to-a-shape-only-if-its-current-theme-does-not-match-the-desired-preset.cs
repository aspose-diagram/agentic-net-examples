using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    // Desired preset theme to apply
    private const PresetThemeValue DesiredTheme = PresetThemeValue.Bubble;
    // Name of the custom property used to track applied theme
    private const string ThemeTrackingPropName = "AppliedTheme";

    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Check if the shape already has the desired theme applied
                    bool themeAlreadyApplied = false;
                    foreach (Prop prop in shape.Props)
                    {
                        if (prop.Name == ThemeTrackingPropName && prop.Value != null && prop.Value.Val == DesiredTheme.ToString())
                        {
                            themeAlreadyApplied = true;
                            break;
                        }
                    }

                    if (themeAlreadyApplied)
                    {
                        // Theme matches; no action needed
                        continue;
                    }

                    // Apply the preset theme to the shape
                    shape.PresetTheme = DesiredTheme;

                    // Optionally, you could also set a variant or quickstyle here
                    // shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                    // shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;

                    // Record the applied theme in a custom property for future checks
                    Prop trackingProp = null;
                    foreach (Prop prop in shape.Props)
                    {
                        if (prop.Name == ThemeTrackingPropName)
                        {
                            trackingProp = prop;
                            break;
                        }
                    }

                    if (trackingProp == null)
                    {
                        // Create a new custom property if it does not exist
                        trackingProp = new Prop();
                        trackingProp.Name = ThemeTrackingPropName;
                        shape.Props.Add(trackingProp);
                    }

                    // Store the theme name as the property value
                    trackingProp.Value.Val = DesiredTheme.ToString();
                }
            }

            // Save the modified diagram to the output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}