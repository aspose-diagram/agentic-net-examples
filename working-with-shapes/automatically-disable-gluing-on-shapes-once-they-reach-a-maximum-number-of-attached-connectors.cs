using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Maximum number of connectors allowed to be glued to a shape.
    const int MaxConnectors = 3;

    static void Main()
    {
        try
        {

            // Load an existing Visio diagram.
            // Replace "input.vsdx" with the path to your diagram file.
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip connector shapes themselves (1‑D shapes).
                    if (shape.OneD)
                        continue;

                    // Retrieve IDs of all 1‑D connectors glued to this shape.
                    long[] gluedConnectorIds = shape.GluedShapes(GluedShapesFlags.GluedShapesAll1D, null, null);

                    // If the number of glued connectors exceeds the allowed maximum,
                    // disable dynamic gluing for this shape.
                    if (gluedConnectorIds != null && gluedConnectorIds.Length > MaxConnectors)
                    {
                        // Set GlueType to prevent further dynamic glue operations.
                        shape.Misc.GlueType.Value = GlueTypeValue.NoAllowDynamicGlue;
                    }
                    else
                    {
                        // Ensure gluing is allowed when under the limit.
                        shape.Misc.GlueType.Value = GlueTypeValue.AllowDynamicGlue;
                    }
                }
            }

            // Save the modified diagram.
            // Replace "output.vsdx" with the desired output file path.
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
