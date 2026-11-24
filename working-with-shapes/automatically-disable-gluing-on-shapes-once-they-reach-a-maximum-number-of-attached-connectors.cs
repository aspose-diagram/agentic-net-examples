using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    // Maximum number of connectors allowed to be glued to a shape.
    private const int MaxGluedConnectors = 3;

    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (Diagram implements IDisposable).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve IDs of all 1‑D (connector) shapes glued to this shape.
                        long[] gluedConnectorIds = shape.GluedShapes(GluedShapesFlags.GluedShapesAll1D, null, null);

                        // If the shape already has the maximum allowed glued connectors,
                        // disable further dynamic gluing for this shape.
                        if (gluedConnectorIds != null && gluedConnectorIds.Length >= MaxGluedConnectors)
                        {
                            // Ensure the Misc collection exists before accessing GlueType.
                            if (shape.Misc != null)
                            {
                                // Set the glue type to disallow dynamic glue.
                                shape.Misc.GlueType.Value = GlueTypeValue.NoAllowDynamicGlue;
                            }
                        }
                    }
                }

                // Save the modified diagram using the correct overload.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram processed and saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}