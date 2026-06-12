using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        // Target theme to apply
        private const PresetThemeValue TargetTheme = PresetThemeValue.Bubble;
        private const PresetThemeVariantValue TargetVariant = PresetThemeVariantValue.Variant1;
        private const PresetQuickStyleValue TargetQuickStyle = PresetQuickStyleValue.VariantStyle1;
        private const string ThemePropName = "AppliedTheme";

        static void Main()
        {
            try
            {

                // Load the Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Check if the shape already has the target theme applied
                        if (HasTargetThemeApplied(shape))
                            continue; // Skip processing for this shape

                        // Apply the theme to the shape
                        shape.PresetTheme = TargetTheme;
                        shape.PresetThemeVariant = TargetVariant;
                        shape.PresetThemeQuickStyle = TargetQuickStyle;

                        // Record that the theme has been applied using a custom property
                        SetAppliedThemeProperty(shape);
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

        // Determines whether the shape already has the target theme applied
        private static bool HasTargetThemeApplied(Shape shape)
        {
            if (shape.Props == null)
                return false;

            foreach (Prop prop in shape.Props)
            {
                if (prop.Name == ThemePropName && prop.Value != null && prop.Value.Val == TargetTheme.ToString())
                {
                    return true;
                }
            }
            return false;
        }

        // Adds or updates the custom property indicating the applied theme
        private static void SetAppliedThemeProperty(Shape shape)
        {
            if (shape.Props == null)
                return;

            // Look for existing property
            foreach (Prop prop in shape.Props)
            {
                if (prop.Name == ThemePropName)
                {
                    prop.Value.Val = TargetTheme.ToString();
                    return;
                }
            }

            // Property not found; create a new one
            Prop newProp = new Prop();
            newProp.Name = ThemePropName;
            newProp.Value.Val = TargetTheme.ToString();
            shape.Props.Add(newProp);
        }
    }