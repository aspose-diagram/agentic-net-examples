using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // Locate the target shape (by universal name "MyShape" in this example)
            Shape targetShape = null;
            foreach (Shape shape in page.Shapes)
            {
                if (shape.NameU != null && shape.NameU.Equals("MyShape", StringComparison.OrdinalIgnoreCase))
                {
                    targetShape = shape;
                    break;
                }
            }

            // If the shape wasn't found by name, fall back to a known shape ID (e.g., 1)
            if (targetShape == null)
            {
                long fallbackId = 1;
                try
                {
                    targetShape = page.Shapes.GetShape(fallbackId);
                }
                catch
                {
                    Console.WriteLine("Target shape not found.");
                    return;
                }
            }

            // Set glue options: allow incoming connectors but disallow outgoing dynamic glue
            targetShape.Misc.GlueType.Value = GlueTypeValue.NoAllowDynamicGlue;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine("Glue options updated and diagram saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
