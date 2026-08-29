using System;
using Aspose.Diagram;

class Program
    {
        // Maximum number of connectors allowed to be glued to a shape.
        const int MaxAllowedConnectors = 3;

        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramGluingControl <input.vsdx> <output.vsdx>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Get all 1‑D connectors glued to this shape.
                    long[] gluedConnectorIds = shape.GluedShapes(GluedShapesFlags.GluedShapesAll1D, null, null);

                    // If the number of glued connectors exceeds the limit, disable further gluing.
                    if (gluedConnectorIds != null && gluedConnectorIds.Length >= MaxAllowedConnectors)
                    {
                        // Disable dynamic glue for this shape.
                        shape.Misc.GlueType.Value = GlueTypeValue.NoAllowDynamicGlue;
                        Console.WriteLine($"Gluing disabled for shape ID {shape.ID} (glued connectors: {gluedConnectorIds.Length}).");
                    }
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
    }