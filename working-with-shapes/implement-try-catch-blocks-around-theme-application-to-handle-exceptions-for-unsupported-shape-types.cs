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

            // Iterate through all pages and their shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    try
                    {
                        // Attempt to apply a preset theme style matrix to the shape.
                        // Adjust the enum values as needed for your scenario.
                        shape.SetPresetThemeStyleMatrics(
                            PresetStyleMatricsValue.Style1,
                            PresetColorMatricsValue.Color1);
                    }
                    catch (Exception ex)
                    {
                        // Handle shapes that do not support theme application.
                        // For example, log the shape ID and continue processing.
                        Console.WriteLine($"Shape ID {shape.ID} unsupported for theme: {ex.Message}");
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
