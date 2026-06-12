using System;
using System.IO;
using Aspose.Diagram;

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

            // Retrieve a shape by its ID (example uses shape ID 1)
            Shape shape = page.Shapes.GetShape(1);

            // Apply a preset theme style matrix:
            //   - Style2 corresponds to the second row of the style matrix
            //   - Color3 corresponds to the third column of the color matrix
            shape.SetPresetThemeStyleMatrics(PresetStyleMatricsValue.Style2, PresetColorMatricsValue.Color3);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
