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

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Original theme cannot be read (write‑only), so we note it as unknown
                    string originalTheme = "Unknown (write‑only property)";

                    // Apply a new preset theme to the shape
                    shape.PresetTheme = PresetThemeValue.Bubble;
                    shape.PresetThemeVariant = PresetThemeVariantValue.Variant2;
                    shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle3;

                    // Build a description of the new theme settings
                    string newTheme = $"Bubble, Variant2, QuickStyle VariantStyle3";

                    // Output the report line for this shape
                    Console.WriteLine($"Shape ID {shape.ID}: Original Theme = {originalTheme}, New Theme = {newTheme}");
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
