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

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Find the first non‑deleted shape on the page
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Del == BOOL.False)
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.WriteLine("No shape found to modify.");
                return;
            }

            // Apply a preset theme to the shape to enhance visual depth
            targetShape.PresetTheme = PresetThemeValue.Bubble;
            targetShape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
            targetShape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle2;

            // Configure the shape's shadow effect
            targetShape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;   // Enable simple shadow
            targetShape.Fill.ShdwForegnd.Value = "#000000";                    // Shadow color (black)
            targetShape.Fill.ShdwForegndTrans.Value = 0.3;                     // 30% transparency
            targetShape.Fill.ShapeShdwOffsetX.Value = 0.1;                     // Horizontal offset (inches)
            targetShape.Fill.ShapeShdwOffsetY.Value = 0.1;                     // Vertical offset (inches)

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Diagram saved with updated shadow effect.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
