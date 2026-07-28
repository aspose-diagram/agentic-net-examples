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

            // Define the fresh preset theme to apply
            PresetThemeValue freshTheme = PresetThemeValue.Office;

            // Remove existing theme settings and apply the fresh theme to each page
            foreach (Page page in diagram.Pages)
            {
                // Clear any existing theme by setting to NoTheme
                page.PresetTheme = PresetThemeValue.NoTheme;
                // Apply the fresh preset theme to the page
                page.PresetTheme = freshTheme;

                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Clear existing theme settings on the shape
                    shape.PresetTheme = PresetThemeValue.NoTheme;
                    // Apply the fresh preset theme to the shape
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
