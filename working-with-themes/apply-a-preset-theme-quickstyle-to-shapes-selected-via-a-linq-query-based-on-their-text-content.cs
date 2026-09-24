using System;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                Diagram diagram = new Diagram("input.vsdx");

                // Define the text to search for (case‑insensitive)
                string searchText = "Target";

                // Select shapes whose plain text contains the search string
                List<Shape> targetShapes = diagram.Pages
                    .SelectMany(page => page.Shapes.Cast<Shape>())
                    .Where(shape =>
                    {
                        string plainText = shape.Text.Value.ToString();
                        return !string.IsNullOrWhiteSpace(plainText) &&
                               plainText.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0;
                    })
                    .ToList();

                // Apply a preset theme quickstyle to each selected shape
                foreach (Shape shape in targetShapes)
                {
                    // Choose any valid theme, variant and quickstyle
                    shape.PresetTheme = PresetThemeValue.Bubble;
                    shape.PresetThemeVariant = PresetThemeVariantValue.Variant2;
                    shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle3;
                }

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }