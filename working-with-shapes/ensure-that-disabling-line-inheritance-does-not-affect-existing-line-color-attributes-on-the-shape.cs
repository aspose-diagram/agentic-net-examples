using System.IO;
using System;
using Aspose.Diagram;

class DisableLineInheritancePreserveColor
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
                    // Ensure the shape has a Line object and a defined LineColor
                    if (shape.Line != null && shape.Line.LineColor != null)
                    {
                        // Preserve the current line color
                        ColorValue originalLineColor = shape.Line.LineColor;

                        // Disable line inheritance by clearing the LineStyle reference
                        shape.LineStyle = null;

                        // Reapply the preserved line color to keep the visual appearance unchanged
                        shape.Line.LineColor = originalLineColor;
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
