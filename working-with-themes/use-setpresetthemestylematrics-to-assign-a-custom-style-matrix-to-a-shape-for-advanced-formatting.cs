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

            // Retrieve a shape by its ID (replace 1 with the actual shape ID you want to format)
            Shape shape = page.Shapes.GetShape(1);

            // Apply a preset theme style matrix to the shape.
            // Example: use Style2 (row) and Color3 (column) from the preset matrices.
            shape.SetPresetThemeStyleMatrics(
                PresetStyleMatricsValue.Style2,
                PresetColorMatricsValue.Color3);

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
