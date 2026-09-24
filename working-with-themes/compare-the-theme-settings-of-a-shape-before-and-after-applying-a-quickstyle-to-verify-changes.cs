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

            // Get the first page and the first shape on that page
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes[0];

            // Capture the fill foreground color before applying a quickstyle
            string fillBefore = shape.Fill.FillForegnd.Value;
            Console.WriteLine($"Fill color before quickstyle: {fillBefore}");

            // Apply a preset theme, variant, and quickstyle to the shape
            shape.PresetTheme = PresetThemeValue.Bubble;
            shape.PresetThemeVariant = PresetThemeVariantValue.Variant2;
            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle3;

            // Capture the fill foreground color after applying the quickstyle
            string fillAfter = shape.Fill.FillForegnd.Value;
            Console.WriteLine($"Fill color after quickstyle: {fillAfter}");

            // Verify that the theme application changed the fill color
            if (fillBefore != fillAfter)
            {
                Console.WriteLine("Theme quickstyle applied successfully: fill color changed.");
            }
            else
            {
                Console.WriteLine("Theme quickstyle may not have taken effect: fill color unchanged.");
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
