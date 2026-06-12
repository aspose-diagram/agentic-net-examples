using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Choose preset style and color indices (example values)
            PresetStyleMatricsValue styleIndex = PresetStyleMatricsValue.Style1;
            PresetColorMatricsValue colorIndex = PresetColorMatricsValue.Color1;

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    try
                    {
                        // Apply the preset theme style matrix to the shape
                        shape.SetPresetThemeStyleMatrics(styleIndex, colorIndex);
                    }
                    catch (Exception ex)
                    {
                        // Handle shapes that do not support theme application
                        Console.WriteLine($"Shape ID {shape.ID} could not apply theme: {ex.Message}");
                    }
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
