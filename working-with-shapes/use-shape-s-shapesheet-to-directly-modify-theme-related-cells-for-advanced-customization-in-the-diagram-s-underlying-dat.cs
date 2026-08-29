using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (you can change the index as needed)
            Page page = diagram.Pages[0];

            // Iterate through all shapes on the page
            foreach (Shape shape in page.Shapes)
            {
                // Skip shapes that are marked as deleted
                if (shape.Del == BOOL.True)
                    continue;

                // Apply theme customizations directly via ShapeSheet‑related properties
                // These properties write to the underlying theme cells of the shape
                shape.PresetTheme = PresetThemeValue.Bubble;
                shape.PresetThemeVariant = PresetThemeVariantValue.Variant2;
                shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle3;
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
