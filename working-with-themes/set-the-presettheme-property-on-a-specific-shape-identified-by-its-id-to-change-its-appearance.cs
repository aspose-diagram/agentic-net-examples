using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path for the modified Visio file
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // ID of the shape whose theme we want to change
            long targetShapeId = 5; // replace with the actual shape ID

            // Retrieve the shape from the first page
            Shape shape = diagram.Pages[0].Shapes.GetShape(targetShapeId);
            if (shape == null)
            {
                throw new Exception($"Shape with ID {targetShapeId} not found.");
            }

            // Apply a preset theme to the shape
            shape.PresetTheme = PresetThemeValue.Bubble;
            // Optionally, set a variant and quick style
            shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
