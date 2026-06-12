using System;
using System.Linq;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                // Replace "input.vsdx" with the actual path to your diagram file
                Diagram diagram = new Diagram("input.vsdx");

                // Define the text fragment to search for
                const string searchText = "Target";

                // Select all shapes whose plain text contains the search fragment
                // Exclude shapes that are marked as deleted (shape.Del == BOOL.True)
                IEnumerable<Shape> matchingShapes = diagram.Pages
                    .Cast<Page>()
                    .SelectMany(page => page.Shapes.Cast<Shape>())
                    .Where(shape => shape.Del == BOOL.False &&
                                    shape.Text.Value.ToString().Contains(searchText));

                // Apply a preset theme, variant, and quickstyle to each matching shape
                foreach (Shape shape in matchingShapes)
                {
                    // Set the theme (write‑only property)
                    shape.PresetTheme = PresetThemeValue.Bubble;

                    // Set the theme variant (write‑only property)
                    shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;

                    // Set the quickstyle (write‑only property)
                    shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle2;
                }

                // Save the modified diagram
                // Replace "output.vsdx" with the desired output path
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }