using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                Diagram diagram = new Diagram("input.vsdx");

                // Access the first page of the diagram
                Page page = diagram.Pages[0];

                // Add a new rectangle shape to the page
                // The fourth parameter (isCalculate) must be a boolean
                long shapeId = page.AddShape(2.0, 2.0, "Rectangle", false);

                // Retrieve the shape instance using the returned ID
                Shape shape = page.Shapes.GetShape(shapeId);

                // -----------------------------------------------------------------
                // Directly modify theme‑related cells via the ShapeSheet properties
                // -----------------------------------------------------------------

                // Change the fill foreground and background colors (hex strings)
                shape.Fill.FillForegnd.Value = "#FF0000"; // Red foreground
                shape.Fill.FillBkgnd.Value = "#00FF00";   // Green background

                // Change the line color and weight
                shape.Line.LineColor.Value = "#0000FF";   // Blue line
                shape.Line.LineWeight.Value = 0.02;       // 0.02 inches

                // Apply a preset theme to the shape (write‑only properties)
                shape.PresetTheme = PresetThemeValue.Bubble;
                shape.PresetThemeVariant = PresetThemeVariantValue.Variant2;
                shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle3;

                // Save the modified diagram to a new file
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }