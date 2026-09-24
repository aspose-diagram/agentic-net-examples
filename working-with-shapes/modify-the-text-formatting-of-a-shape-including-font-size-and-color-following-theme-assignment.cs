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

            // Access the first page (index 0)
            Page page = diagram.Pages[0];

            // Retrieve a shape by its ID (example uses ID 1)
            Shape shape = page.Shapes.GetShape(1);

            // Replace any existing text with new content
            shape.Text.Value.Clear();
            shape.Text.Value.Add(new Txt("Sample Text"));

            // Create a character formatting entry for the text run
            Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
            ch.IX = 0; // index of the first character run
            ch.FontName.Value = "Calibri";
            // Convert font size from points to inches (1 point = 1/72 inch)
            double fontSizePoints = 24;
            ch.Size.Value = fontSizePoints / 72.0;
            // Set text color using a hex string
            ch.Color.Value = "#FF0000"; // Red
            // Apply bold style (preserves other styles if needed)
            ch.Style.Value = StyleValue.Bold;
            // Add the character formatting to the shape
            shape.Chars.Add(ch);

            // Apply a preset theme to the shape
            shape.PresetTheme = PresetThemeValue.Bubble;
            shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle2;

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
