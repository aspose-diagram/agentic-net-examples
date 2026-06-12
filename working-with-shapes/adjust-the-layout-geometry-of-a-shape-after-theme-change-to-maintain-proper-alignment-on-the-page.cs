using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {

            // Paths for input and output Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (you can adjust for other pages as needed)
            Page page = diagram.Pages[0];

            // Store original positions of each shape to preserve alignment after theme change
            var originalPositions = new Dictionary<long, (double PinX, double PinY)>();
            foreach (Shape shape in page.Shapes)
            {
                // Skip shapes that are marked as deleted
                if (shape.Del == BOOL.True)
                    continue;

                originalPositions[shape.ID] = (shape.XForm.PinX.Value, shape.XForm.PinY.Value);
            }

            // Apply a preset theme to the page (example: Bubble theme with Variant2)
            page.PresetTheme = PresetThemeValue.Bubble;
            page.PresetThemeVariant = PresetThemeVariantValue.Variant2;

            // Re‑apply the stored positions to keep shapes aligned as before the theme change
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Del == BOOL.True)
                    continue;

                if (originalPositions.TryGetValue(shape.ID, out var pos))
                {
                    shape.XForm.PinX.Value = pos.PinX;
                    shape.XForm.PinY.Value = pos.PinY;
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
