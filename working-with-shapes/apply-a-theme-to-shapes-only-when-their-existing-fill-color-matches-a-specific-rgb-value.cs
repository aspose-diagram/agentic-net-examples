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
                // Path for the modified Visio file
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the target fill color (hex string, case-insensitive)
                const string targetFillColor = "#FF0000"; // Red

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the current fill foreground color
                        string currentFill = shape.Fill.FillForegnd.Value;

                        // Compare colors ignoring case
                        if (!string.IsNullOrEmpty(currentFill) && 
                            string.Equals(currentFill, targetFillColor, StringComparison.OrdinalIgnoreCase))
                        {
                            // Apply a preset theme to the matching shape
                            shape.PresetTheme = PresetThemeValue.Bubble;
                            shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;
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