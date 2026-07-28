using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load an existing Visio diagram
            Aspose.Diagram.Diagram diagram = new Aspose.Diagram.Diagram("input.vsdx");

            // Get the shape you want to modify (for example, the first shape on the first page)
            Aspose.Diagram.Shape shape = diagram.Pages[0].Shapes[1];

            // Preserve the current line color before changing inheritance settings
            Aspose.Diagram.ColorValue originalLineColor = shape.Line.LineColor;

            // Disable line inheritance by removing the line style reference
            // This prevents the shape from inheriting line formatting from a master or style sheet
            shape.LineStyle = null;

            // Re‑apply the preserved line color to ensure it is not lost after inheritance is disabled
            shape.Line.LineColor = originalLineColor;

            // Save the modified diagram
            diagram.Save("output.vsdx", Aspose.Diagram.SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
