using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input file, output file, optional max connectors
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx> [maxConnectors]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        int maxConnectors = 3; // default limit

        if (args.Length >= 3 && !int.TryParse(args[2], out maxConnectors))
        {
            Console.Error.WriteLine("Invalid maxConnectors value. It must be an integer.");
            return;
        }

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block (required pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Get all 1‑D connectors glued to this shape
                        long[] gluedConnectorIds = shape.GluedShapes(GluedShapesFlags.GluedShapesAll1D, null, null);

                        // If the number of glued connectors exceeds the limit, disable further gluing
                        if (gluedConnectorIds != null && gluedConnectorIds.Length > maxConnectors)
                        {
                            Console.WriteLine($"Shape ID {shape.ID} has {gluedConnectorIds.Length} glued connectors (limit {maxConnectors}). Disabling dynamic glue.");

                            // Set the shape's glue type to prevent additional dynamic gluing
                            shape.Misc.GlueType.Value = GlueTypeValue.NoAllowDynamicGlue;
                        }
                    }
                }

                // Save the modified diagram to the output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram processed and saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}