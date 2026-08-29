using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio file (create/load rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Get the first page
            Page page = diagram.Pages[0];

            // Get a shape from the page (skip the background shape with ID 1)
            Shape shape = page.Shapes[1];

            // Apply a preset theme style matrix:
            //   - Style row: Style3
            //   - Color column: Color4
            shape.SetPresetThemeStyleMatrics(PresetStyleMatricsValue.Style3, PresetColorMatricsValue.Color4);

            // Save the modified diagram (save rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
