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
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // Retrieve the shape you want to modify (example uses shape ID = 1)
            long shapeId = 1;
            Shape shape = page.Shapes.GetShape(shapeId);

            // Disable dynamic gluing for this shape to prevent further connector attachments
            shape.Misc.GlueType.Value = GlueTypeValue.NoAllowDynamicGlue;

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
