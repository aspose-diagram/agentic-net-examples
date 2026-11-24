using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify connector shapes (1‑D shapes). OneD is a native bool.
                        if (shape.OneD)
                        {
                            long sourceShapeId = -1;

                            // Find the connection that represents the source (BeginX) of the connector.
                            foreach (Connect conn in page.Connects)
                            {
                                // The connector appears as ToSheet in the Connect collection.
                                // The BeginX cell indicates the source end of the connector.
                                if (conn.ToSheet == shape.ID &&
                                    conn.FromCell != null &&
                                    conn.FromCell.Contains("BeginX"))
                                {
                                    sourceShapeId = conn.FromSheet;
                                    break;
                                }
                            }

                            // If a source shape was found, examine it.
                            if (sourceShapeId != -1)
                            {
                                Shape sourceShape = page.Shapes.GetShape(sourceShapeId);
                                if (sourceShape != null &&
                                    sourceShape.Del == BOOL.False &&                     // Skip deleted shapes.
                                    sourceShape.Master != null &&
                                    sourceShape.Master.Name == "Decision")               // Identify decision node by master name.
                                {
                                    // Set the line jump style of the connector to Arc.
                                    shape.SetConnectorJumpValue(ConLineJumpCodeValue.Always, ConLineJumpStyleValue.Arc);
                                }
                            }
                        }
                    }
                }

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}