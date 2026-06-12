using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Check if the shape's fill foreground color is defined (theme matrix contains a color)
                        string foreColor = shape.Fill.FillForegnd.Value;
                        if (!string.IsNullOrWhiteSpace(foreColor))
                        {
                            // Apply a preset theme and a specific variant to the shape
                            shape.PresetTheme = PresetThemeValue.Bubble;
                            shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
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