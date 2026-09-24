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

            // Assume the shape we want to modify is on the first page and has the universal name "MyShape"
            Page page = diagram.Pages[0];
            Shape targetShape = null;

            foreach (Shape shape in page.Shapes)
            {
                if (shape.NameU == "MyShape")
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                throw new Exception("Shape with NameU 'MyShape' not found.");
            }

            // Apply a preset theme to the shape
            targetShape.PresetTheme = PresetThemeValue.Bubble;
            targetShape.PresetThemeVariant = PresetThemeVariantValue.Variant1;

            // After applying the theme, update line weight and dash pattern
            // Line weight is in inches; here we set it to 0.02 inches
            targetShape.Line.LineWeight.Value = 0.02;

            // Set dash pattern using the LinePatternValue enum (e.g., Dash)
            targetShape.Line.LinePattern.Value = LinePatternValue.Dash;

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
