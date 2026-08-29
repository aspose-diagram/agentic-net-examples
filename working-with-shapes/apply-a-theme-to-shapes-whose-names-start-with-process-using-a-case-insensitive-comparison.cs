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
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and their shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Apply theme only to shapes whose name starts with "Process" (case‑insensitive)
                    if (!string.IsNullOrEmpty(shape.NameU) &&
                        shape.NameU.StartsWith("Process", StringComparison.OrdinalIgnoreCase))
                    {
                        // Set a preset theme (e.g., Office) for the shape
                        shape.PresetTheme = PresetThemeValue.Office;

                        // Optionally apply a quick style variant to enhance appearance
                        shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;
                    }
                }
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
