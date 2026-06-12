using System.IO;
using System;
using Aspose.Diagram;

class ThemeStandardizer
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Remove any existing theme settings from pages and shapes
            foreach (Page page in diagram.Pages)
            {
                page.PresetTheme = PresetThemeValue.NoTheme;

                foreach (Shape shape in page.Shapes)
                {
                    shape.PresetTheme = PresetThemeValue.NoTheme;
                    // Optional: clear quick style and variant if they were set
                    // shape.PresetThemeQuickStyle = 0;
                    // shape.PresetThemeVariant = 0;
                }
            }

            // Apply a fresh preset theme (e.g., Office) to all pages and shapes
            PresetThemeValue freshTheme = PresetThemeValue.Office;

            foreach (Page page in diagram.Pages)
            {
                page.PresetTheme = freshTheme;

                foreach (Shape shape in page.Shapes)
                {
                    shape.PresetTheme = freshTheme;
                }
            }

            // Save the updated diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
