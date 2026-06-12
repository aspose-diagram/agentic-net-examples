using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Retrieve the first page
            Page page = diagram.Pages[0];

            // Retrieve a shape from the page (index may be adjusted as needed)
            Shape shape = page.Shapes[1];

            // Apply the preset theme "Bubble" to the shape
            shape.PresetTheme = PresetThemeValue.Bubble;

            // Confirm that the theme was set (property is write‑only, so we rely on successful assignment)
            Console.WriteLine($"Preset theme 'Bubble' applied to shape with ID {shape.ID}.");

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
