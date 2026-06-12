using System.IO;
using System;
using Aspose.Diagram;

class ThemeGeometryValidator
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Assume we work with the first page and the first shape on that page
            Page page = diagram.Pages[0];
            Shape shape = page.Shapes[0];

            // Capture original geometry
            double originalPinX = shape.XForm.PinX.Value;
            double originalPinY = shape.XForm.PinY.Value;
            double originalWidth = shape.XForm.Width.Value;
            double originalHeight = shape.XForm.Height.Value;

            // Apply a preset theme to the shape (e.g., Office theme)
            shape.PresetTheme = PresetThemeValue.Office;

            // Optionally, apply a quick style or variant if needed
            // shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;
            // shape.PresetThemeVariant = PresetThemeVariantValue.Style1;

            // Capture geometry after applying the theme
            double newPinX = shape.XForm.PinX.Value;
            double newPinY = shape.XForm.PinY.Value;
            double newWidth = shape.XForm.Width.Value;
            double newHeight = shape.XForm.Height.Value;

            // Compare the geometry values
            bool geometryUnchanged =
                Math.Abs(originalPinX - newPinX) < 0.0001 &&
                Math.Abs(originalPinY - newPinY) < 0.0001 &&
                Math.Abs(originalWidth - newWidth) < 0.0001 &&
                Math.Abs(originalHeight - newHeight) < 0.0001;

            // Output the result
            Console.WriteLine("Geometry unchanged after applying preset theme: " + geometryUnchanged);

            // Save the modified diagram (optional)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
