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

            // Load the diagram file
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Define the shape IDs that should exist
            long[] expectedShapeIds = { 1, 2, 3, 4, 5 };

            // Work with the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // Check each expected ID
            foreach (long shapeId in expectedShapeIds)
            {
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.WriteLine($"Missing shape with ID: {shapeId}");
                    continue;
                }

                // Apply a preset theme to the existing shape
                shape.PresetTheme = PresetThemeValue.Bubble;
                shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
            }

            // Apply a preset theme to the whole page
            page.PresetTheme = PresetThemeValue.Bubble;
            page.PresetThemeVariant = PresetThemeVariantValue.Variant1;

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
