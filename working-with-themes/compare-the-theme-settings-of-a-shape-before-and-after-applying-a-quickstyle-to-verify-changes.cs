using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with actual file path)
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Access the first page and the first shape on that page
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes.GetShape(1); // assumes shape with ID 1 exists

            // Capture theme‑related visual properties before applying a quickstyle
            string originalFillColor = shape.Fill.FillForegnd.Value;
            string originalLineColor = shape.Line.LineColor.Value;

            // Apply a preset theme, variant, and quickstyle to the shape
            shape.PresetTheme = PresetThemeValue.Bubble;
            shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle2;

            // Capture the same properties after the quickstyle is applied
            string newFillColor = shape.Fill.FillForegnd.Value;
            string newLineColor = shape.Line.LineColor.Value;

            // Verify that the visual properties have changed
            if (originalFillColor == newFillColor && originalLineColor == newLineColor)
            {
                throw new Exception("Quickstyle did not modify the shape's theme‑related properties.");
            }
            else
            {
                Console.WriteLine("Quickstyle applied successfully.");
                Console.WriteLine($"Fill color changed from {originalFillColor} to {newFillColor}");
                Console.WriteLine($"Line color changed from {originalLineColor} to {newLineColor}");
            }

            // Save the modified diagram to verify persistence (optional)
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
