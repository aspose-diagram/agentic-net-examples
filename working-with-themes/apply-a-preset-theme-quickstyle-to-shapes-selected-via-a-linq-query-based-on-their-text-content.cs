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

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Define the text to search for (case‑insensitive)
            string searchText = "Target";

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // LINQ query to select shapes whose visible text contains the search string
                var matchingShapes = page.Shapes
                    .Cast<Shape>()
                    .Where(s => s.Del == BOOL.False && 
                                !string.IsNullOrWhiteSpace(s.Text.Value.ToString()) &&
                                s.Text.Value.ToString().IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0);

                // Apply a preset theme quickstyle to each matching shape
                foreach (Shape shape in matchingShapes)
                {
                    shape.PresetTheme = PresetThemeValue.Bubble;                     // Apply the theme
                    shape.PresetThemeVariant = PresetThemeVariantValue.Variant2;    // Choose a variant
                    shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle3; // Apply a quickstyle
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
