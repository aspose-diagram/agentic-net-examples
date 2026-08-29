using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram from the file system
            Diagram diagram = new Diagram(inputPath);

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Find the first non‑deleted shape on the page
            Shape targetShape = null;
            foreach (Shape s in page.Shapes)
            {
                if (s.Del == BOOL.False)
                {
                    targetShape = s;
                    break;
                }
            }

            if (targetShape == null)
            {
                throw new Exception("No non‑deleted shape found on the first page.");
            }

            // Apply a preset theme to the shape
            targetShape.PresetTheme = PresetThemeValue.Bubble;
            targetShape.PresetThemeVariant = PresetThemeVariantValue.Variant1;

            // Update line weight (in inches) and dash pattern after the theme is applied
            targetShape.Line.LineWeight.Value = 0.05;               // 0.05 inches thick
            targetShape.Line.LinePattern.Value = LinePatternValue.Dash; // Dashed line

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
