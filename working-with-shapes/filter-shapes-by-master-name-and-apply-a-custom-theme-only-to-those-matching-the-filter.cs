using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Master name to filter shapes by
            string targetMasterName = "MyCustomMaster";

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape is based on a master and if the master's name matches the filter
                    if (shape.Master != null && shape.Master.NameU == targetMasterName)
                    {
                        // Apply a preset theme style matrix to the shape
                        // (using example enum values; replace with desired style/color)
                        shape.SetPresetThemeStyleMatrics(PresetStyleMatricsValue.Style1, PresetColorMatricsValue.Color1);
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
