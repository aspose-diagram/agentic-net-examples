using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load an existing Visio diagram
            Aspose.Diagram.Diagram diagram = new Aspose.Diagram.Diagram("input.vsdx");

            // Iterate through all pages and shapes
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Check if the shape name starts with "Process" (case‑insensitive)
                    if (!string.IsNullOrEmpty(shape.Name) &&
                        shape.Name.StartsWith("Process", System.StringComparison.OrdinalIgnoreCase))
                    {
                        // Apply a preset theme (e.g., Office) to the shape
                        shape.PresetTheme = Aspose.Diagram.PresetThemeValue.Office;

                        // Optionally set a quick style variant
                        shape.PresetThemeQuickStyle = Aspose.Diagram.PresetQuickStyleValue.VariantStyle1;
                    }
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", Aspose.Diagram.SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
