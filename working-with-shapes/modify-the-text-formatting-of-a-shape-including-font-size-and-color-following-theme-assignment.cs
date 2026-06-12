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
            using (Diagram diagram = new Diagram("input.vsdx"))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Example condition: modify shape with a specific universal name
                        if (shape.NameU != null && shape.NameU.Equals("TargetShape", StringComparison.OrdinalIgnoreCase))
                        {
                            // Ensure the shape has some text; add if empty
                            if (string.IsNullOrWhiteSpace(shape.Text.Value.ToString()))
                            {
                                shape.Text.Value.Clear();
                                shape.Text.Value.Add(new Txt("Sample Text"));
                            }

                            // Create a new character formatting run
                            Aspose.Diagram.Char ch = new Aspose.Diagram.Char();

                            // Apply to the first character (index 0)
                            ch.IX = 0;

                            // Set font name (optional, can be omitted if theme provides it)
                            ch.FontName.Value = "Calibri";

                            // Set font size: 12 points -> inches (12 / 72)
                            ch.Size.Value = 12.0 / 72.0;

                            // Set font color to red
                            ch.Color.Value = "#FF0000";

                            // Preserve existing styles (if any) and add Bold as an example
                            ch.Style.Value = StyleValue.Bold;

                            // Add the character formatting to the shape
                            shape.Chars.Add(ch);

                            // Apply a preset theme to the shape (theme assignment)
                            shape.PresetTheme = PresetThemeValue.Bubble;
                            shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                            shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;

                            // No further processing needed for this shape
                            break;
                        }
                    }
                }

                // Save the modified diagram in VSDX format
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
