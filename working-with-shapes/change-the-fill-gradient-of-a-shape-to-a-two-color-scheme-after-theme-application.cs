using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {

            // Input and output file paths (adjust as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Apply a preset theme to the first page
            Page page = diagram.Pages[0];
            page.PresetTheme = PresetThemeValue.Bubble;
            page.PresetThemeVariant = PresetThemeVariantValue.Variant1;

            // Locate the first shape on the page
            Shape targetShape = null;
            foreach (Shape s in page.Shapes)
            {
                targetShape = s;
                break;
            }

            if (targetShape == null)
            {
                Console.WriteLine("No shape found on the page.");
                return;
            }

            // Configure a two‑color gradient fill for the shape
            targetShape.Fill.FillPattern.Value = 25; // Gradient fill pattern
            targetShape.Fill.GradientFill.GradientEnabled.Value = BOOL.True;
            targetShape.Fill.GradientFill.GradientDir.Value = 0; // Left‑to‑right direction

            // Clear any existing gradient stops
            targetShape.Fill.GradientFill.GradientStops.Clear();

            // Add gradient stops: start with red, end with green
            targetShape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0, MeasureConst.NUM),
                new ColorValue("#FF0000", MeasureConst.Undefined));

            targetShape.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(1, MeasureConst.NUM),
                new ColorValue("#00FF00", MeasureConst.Undefined));

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
