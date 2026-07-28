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
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Get the first page
            Page page = diagram.Pages[0];

            // Find the first non-deleted shape on the page
            Shape targetShape = null;
            foreach (Shape shp in page.Shapes)
            {
                if (shp.Del == BOOL.False)
                {
                    targetShape = shp;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.WriteLine("No shape found to modify.");
                return;
            }

            // Apply a preset theme to the shape
            targetShape.PresetTheme = PresetThemeValue.Bubble;
            targetShape.PresetThemeVariant = PresetThemeVariantValue.Variant1;

            // Configure shadow effect
            // Enable simple shadow
            targetShape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;
            // Set shadow color (dark gray)
            targetShape.Fill.ShdwForegnd.Value = "#808080";
            // Set shadow transparency (0 = opaque, 1 = fully transparent)
            targetShape.Fill.ShdwForegndTrans.Value = 0.4; // 40% transparent
            // Set shadow offsets
            targetShape.Fill.ShapeShdwOffsetX.Value = 0.2; // horizontal offset in inches
            targetShape.Fill.ShapeShdwOffsetY.Value = 0.2; // vertical offset in inches

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram saved with updated shadow to '{outputPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
