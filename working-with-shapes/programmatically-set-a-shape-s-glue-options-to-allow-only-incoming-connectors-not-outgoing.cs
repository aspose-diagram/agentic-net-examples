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

            // Work with the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // Locate the shape you want to modify.
            // Here we search by the universal name "MyShape".
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.NameU == "MyShape")
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.WriteLine("Target shape not found.");
                return;
            }

            // Set glue options:
            // Allow incoming connectors (default) and disallow outgoing dynamic glue.
            // This is done via the Misc.GlueType cell.
            targetShape.Misc.GlueType.Value = GlueTypeValue.NoAllowDynamicGlue;

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Glue options updated and diagram saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
