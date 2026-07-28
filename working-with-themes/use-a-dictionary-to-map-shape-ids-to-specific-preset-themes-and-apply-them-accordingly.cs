using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class ApplyPresetThemes
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            // (Replace with your actual file path)
            Diagram diagram = new Diagram(@"C:\Input\SampleDiagram.vsdx");

            // Dictionary mapping shape IDs to desired preset theme values
            // Add entries as needed: {shapeId, PresetThemeValue}
            var shapeThemeMap = new Dictionary<long, PresetThemeValue>
            {
                { 5, PresetThemeValue.Office },
                { 12, PresetThemeValue.Linear },
                { 23, PresetThemeValue.Zephyr }
                // Add more mappings here
            };

            // Iterate through all pages and shapes in the diagram
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the current shape ID has a preset theme defined
                    if (shapeThemeMap.TryGetValue(shape.ID, out PresetThemeValue theme))
                    {
                        // Apply the preset theme to the shape
                        shape.PresetTheme = theme;

                        // Optionally, also set a quick style or style matrix if required
                        // Example: set quick style variant 1
                        shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;

                        // Example: set style matrix (row 2, column 3)
                        shape.SetPresetThemeStyleMatrics(PresetStyleMatricsValue.Style2, PresetColorMatricsValue.Color3);
                    }
                }
            }

            // Save the modified diagram
            // (Replace with your desired output path and format)
            diagram.Save(@"C:\Output\SampleDiagram_Themed.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
