using System.IO;
using System;
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

            // Apply a preset theme to the first page (optional, enhances visual style)
            Page page = diagram.Pages[0];
            page.PresetTheme = PresetThemeValue.Bubble;
            page.PresetThemeVariant = PresetThemeVariantValue.Variant1;

            // Retrieve the first shape on the page
            Shape shape = page.Shapes[0];

            // Ensure the shape is not marked for deletion
            if (shape.Del == BOOL.False)
            {
                // Apply a simple shadow effect
                shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;   // Enable simple shadow
                shape.Fill.ShdwForegnd.Value = "#000000";                    // Shadow color (black)
                shape.Fill.ShdwForegndTrans.Value = 0.3;                     // 30% transparency
                shape.Fill.ShapeShdwOffsetX.Value = 0.1;                     // Horizontal offset
                shape.Fill.ShapeShdwOffsetY.Value = 0.1;                     // Vertical offset
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
