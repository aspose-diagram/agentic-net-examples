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

                // Get the first page (index 0)
                Page page = diagram.Pages[0];

                // Find a shape to modify (example: first shape with NameU "Rectangle")
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU != null && shape.NameU.Equals("Rectangle", StringComparison.OrdinalIgnoreCase))
                    {
                        targetShape = shape;
                        break;
                    }
                }

                // If the shape was not found, fallback to the first shape on the page
                if (targetShape == null && page.Shapes.Count > 0)
                {
                    targetShape = page.Shapes.GetShape(page.Shapes[0].ID);
                }

                if (targetShape == null)
                {
                    Console.WriteLine("No shape found to modify.");
                    return;
                }

                // Ensure the shape is not deleted
                if (targetShape.Del == BOOL.True)
                {
                    Console.WriteLine("Target shape is marked as deleted. Skipping glue option update.");
                    return;
                }

                // Set glue option to allow only incoming connectors (disable outgoing dynamic glue)
                // GlueTypeValue.NoAllowDynamicGlue disables outgoing dynamic glue.
                targetShape.Misc.GlueType.Value = GlueTypeValue.NoAllowDynamicGlue;

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Glue option updated and diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }