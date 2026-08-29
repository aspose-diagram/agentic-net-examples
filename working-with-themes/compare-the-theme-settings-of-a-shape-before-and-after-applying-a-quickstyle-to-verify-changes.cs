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

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Get the first page
            Page page = diagram.Pages[0];

            // Ensure there is at least one shape on the page
            if (page.Shapes.Count == 0)
            {
                Console.WriteLine("No shapes found on the first page.");
                return;
            }

            // Retrieve the first shape
            Shape shape = null;
            foreach (Shape s in page.Shapes)
            {
                shape = s;
                break;
            }

            // Capture theme‑related visual properties before applying quickstyle
            string beforeFill = shape.Fill.FillForegnd.Value;
            string beforeLineColor = shape.Line.LineColor.Value;

            // Apply a preset theme, variant, and quickstyle to the shape
            shape.PresetTheme = PresetThemeValue.Bubble;
            shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle2;

            // Capture the same properties after applying the quickstyle
            string afterFill = shape.Fill.FillForegnd.Value;
            string afterLineColor = shape.Line.LineColor.Value;

            // Compare the before and after values
            if (beforeFill == afterFill && beforeLineColor == afterLineColor)
            {
                throw new Exception("Quickstyle application did not change the shape's visual theme properties.");
            }
            else
            {
                Console.WriteLine("Quickstyle applied successfully.");
                Console.WriteLine($"Fill color changed from {beforeFill} to {afterFill}");
                Console.WriteLine($"Line color changed from {beforeLineColor} to {afterLineColor}");
            }

            // Optionally save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
