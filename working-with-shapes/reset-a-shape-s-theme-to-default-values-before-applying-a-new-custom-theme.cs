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

                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Retrieve a shape by its ID (example ID = 1)
                // Adjust the ID as needed for your diagram
                Shape shape = page.Shapes.GetShape(1);
                if (shape == null)
                {
                    throw new Exception("Shape with ID 1 not found.");
                }

                // -------------------------------------------------
                // Reset the shape's theme to default (no theme)
                // Since the theme properties are write‑only, we assign
                // the default values defined by the library.
                // -------------------------------------------------
                shape.PresetTheme = PresetThemeValue.Clouds;               // default theme placeholder
                shape.PresetThemeVariant = PresetThemeVariantValue.Variant1; // default variant
                shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1; // default quick style

                // -------------------------------------------------
                // Apply a new custom theme to the shape
                // Example: use style matrix 2 and color matrix 5
                // -------------------------------------------------
                shape.SetPresetThemeStyleMatrics(PresetStyleMatricsValue.Style2, PresetColorMatricsValue.Color5);

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