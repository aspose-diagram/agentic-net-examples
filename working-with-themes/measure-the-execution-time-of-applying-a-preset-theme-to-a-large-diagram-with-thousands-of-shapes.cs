using System.IO;
using System;
using System.Diagnostics;
using Aspose.Diagram;

class ThemeApplicationTimer
{
    static void Main()
    {
        try
        {

            // Load the source diagram (large diagram with thousands of shapes)
            Diagram diagram = new Diagram("LargeDiagram.vsdx");

            // Choose the preset theme to apply (e.g., Office theme)
            PresetThemeValue themeToApply = PresetThemeValue.Office;

            // Start timing
            Stopwatch sw = Stopwatch.StartNew();

            // Apply the preset theme to every shape in every page
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Apply the selected preset theme
                    shape.PresetTheme = themeToApply;
                }
            }

            // Stop timing
            sw.Stop();

            // Output the elapsed time
            Console.WriteLine($"Applying preset theme '{themeToApply}' to all shapes took: {sw.Elapsed.TotalSeconds} seconds.");

            // Save the modified diagram (optional)
            diagram.Save("LargeDiagram_Themed.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
