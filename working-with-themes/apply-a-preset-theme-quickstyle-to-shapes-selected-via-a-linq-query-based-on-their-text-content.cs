using System.IO;
using System;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the source Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Text to search for within shape texts
            string searchText = "Target";

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // LINQ query: select shapes whose plain text contains the search text (case‑insensitive)
                var matchingShapes = page.Shapes
                    .Cast<Shape>()
                    .Where(s => !string.IsNullOrWhiteSpace(s.Text.Value.ToString()) &&
                                s.Text.Value.ToString().Contains(searchText, StringComparison.OrdinalIgnoreCase));

                // Apply the preset theme quickstyle to each matching shape
                foreach (Shape shape in matchingShapes)
                {
                    shape.PresetTheme = PresetThemeValue.Bubble;
                    shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                    shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle2;
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
}
