using System;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
    {
        // Maximum number of connectors allowed to be glued to a shape
        const int MaxAllowedConnectors = 3;

        static void Main(string[] args)
        {
            // Expect input and output file paths as arguments
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramGlueControl <inputVisioFile> <outputVisioFile>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Skip connector shapes themselves (1-D shapes)
                    if (shape.OneD)
                        continue;

                    // Get IDs of connectors glued to this shape
                    long[] gluedConnectorIds = shape.GluedShapes(GluedShapesFlags.GluedShapesAll1D, null, null);

                    int gluedCount = gluedConnectorIds?.Length ?? 0;

                    // If the number of glued connectors exceeds the limit, disable dynamic glue
                    if (gluedCount >= MaxAllowedConnectors)
                    {
                        // Set GlueType to NoAllowDynamicGlue to prevent further gluing
                        shape.Misc.GlueType.Value = GlueTypeValue.NoAllowDynamicGlue;
                        Console.WriteLine($"Shape ID {shape.ID} glue disabled (attached connectors: {gluedCount}).");
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
    }