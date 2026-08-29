using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing diagram (use the provided load rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Assume we work with the first shape on the first page
            Shape shape = diagram.Pages[0].Shapes[0];

            try
            {
                // Attempt to apply a preset theme style matrix.
                // This will throw if the shape does not contain style data.
                shape.SetPresetThemeStyleMatrics(
                    PresetStyleMatricsValue.Style1,
                    PresetColorMatricsValue.Color1);
            }
            catch (Exception ex)
            {
                // Handle the situation where the shape cannot accept a theme.
                Console.WriteLine($"Error applying theme to shape ID {shape.ID}: {ex.Message}");
            }

            // Save the diagram (use the provided save rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
