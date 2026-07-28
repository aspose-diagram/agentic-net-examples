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

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Filter shapes that are rectangles (geometry type)
                    // In Visio, rectangle shapes often have the NameU "Rectangle"
                    if (shape.NameU == "Rectangle")
                    {
                        // Apply a preset theme quickstyle to the rectangle shape
                        shape.PresetThemeQuickStyle = PresetQuickStyleValue.VariantStyle1;

                        // Alternatively, you can use the matrix method:
                        // shape.SetPresetThemeStyleMatrics(PresetStyleMatricsValue.Style1, PresetColorMatricsValue.Color1);
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
