using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path for the modified Visio file
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Maximum number of connectors allowed to be glued to a shape
                int maxConnectors = 3;

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip connector shapes themselves (1‑D shapes)
                        if (shape.OneD)
                            continue;

                        // Get IDs of all 1‑D connectors glued to this shape
                        long[] gluedConnectorIds = shape.GluedShapes(GluedShapesFlags.GluedShapesAll1D, null, null);

                        // If the number of glued connectors reaches the limit, disable further gluing
                        if (gluedConnectorIds != null && gluedConnectorIds.Length >= maxConnectors)
                        {
                            // Set glue type to disallow dynamic gluing
                            shape.Misc.GlueType.Value = GlueTypeValue.NoAllowDynamicGlue;
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }