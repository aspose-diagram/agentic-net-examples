using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram (replace with the provided load rule if needed)
            Diagram diagram = new Diagram("input.vsdx");

            // Choose the preset theme to apply
            PresetThemeValue themeToApply = PresetThemeValue.Office;

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Apply theme only to shapes whose name starts with "Process" (case‑insensitive)
                    if (!string.IsNullOrEmpty(shape.NameU) &&
                        shape.NameU.StartsWith("Process", StringComparison.OrdinalIgnoreCase))
                    {
                        shape.PresetTheme = themeToApply;
                        // Optional: set a quick‑style variant as well
                        shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;
                    }
                }
            }

            // Save the modified diagram (replace with the provided save rule if needed)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
