using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Example condition: apply changes to the first shape found
                        // Replace this with your own logic (e.g., check shape.NameU)
                        // Apply a preset theme to the shape
                        shape.PresetTheme = PresetThemeValue.Bubble;
                        shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                        shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;

                        // After the theme is applied, update line weight and dash pattern
                        shape.Line.LineWeight.Value = 0.05; // line weight in inches
                        shape.Line.LinePattern.Value = LinePatternValue.Dash; // dash pattern

                        // Exit after processing the first shape (remove if you want to process all)
                        break;
                    }
                    // Exit after processing the first page (remove if you want to process all pages)
                    break;
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
    }