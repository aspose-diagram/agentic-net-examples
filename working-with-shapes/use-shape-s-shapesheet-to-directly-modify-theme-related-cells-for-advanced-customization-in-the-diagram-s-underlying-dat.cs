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

            // Verify that the page contains at least one shape
            if (page.Shapes.Count == 0)
            {
                Console.WriteLine("The first page does not contain any shapes.");
                return;
            }

            // Retrieve the first shape by its ID
            long firstShapeId = page.Shapes[0].ID;
            Shape shape = page.Shapes.GetShape(firstShapeId);
            if (shape == null)
            {
                Console.WriteLine("Failed to retrieve the shape.");
                return;
            }

            // ----- Directly modify theme‑related cells via the ShapeSheet -----
            // Change the fill foreground color (theme‑related fill cell)
            shape.Fill.FillForegnd.Value = "#FF5733"; // Custom orange‑red color

            // Change the line color (theme‑related line cell)
            shape.Line.LineColor.Value = "#3366FF"; // Custom blue color

            // Adjust the line weight (thickness) in inches
            shape.Line.LineWeight.Value = 0.02;

            // Apply a preset theme to the shape (writes to the theme cells)
            shape.PresetTheme = PresetThemeValue.Bubble;
            shape.PresetThemeVariant = PresetThemeVariantValue.Variant2;
            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle3;

            // Save the modified diagram to a new file
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
