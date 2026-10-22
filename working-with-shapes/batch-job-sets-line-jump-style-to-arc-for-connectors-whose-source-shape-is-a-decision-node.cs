using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input folder and output folder.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFolder> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        // Guard against missing input folder.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure output folder exists.
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {ex.Message}");
                return;
            }
        }

        // Process each Visio file in the input folder.
        string[] files = Directory.GetFiles(inputFolder, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in files)
        {
            // Consider only Visio file extensions.
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (ext != ".vsdx" && ext != ".vsd" && ext != ".vsdm")
                continue;

            // Guard against missing file (should not happen after GetFiles, but keep per rules).
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram inside a using block (required pattern).
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Iterate through all pages.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes.
                            if (shape.Del == BOOL.True)
                                continue;

                            // Identify connector shapes (1‑D shapes). shape.OneD is a bool, not BOOL.
                            if (shape.OneD)
                            {
                                // Find the connection that represents the source (Begin) of this connector.
                                Connect sourceConnect = null;
                                foreach (Connect conn in page.Connects)
                                {
                                    // The connector shape ID appears in ToSheet; the source shape ID is in FromSheet.
                                    // The source cell for the beginning of a connector is typically "BeginX".
                                    if (conn.ToSheet == shape.ID && conn.FromCell != null && conn.FromCell.StartsWith("BeginX", StringComparison.OrdinalIgnoreCase))
                                    {
                                        sourceConnect = conn;
                                        break;
                                    }
                                }

                                if (sourceConnect == null)
                                    continue; // No source connection found.

                                // Retrieve the source shape.
                                Shape sourceShape = page.Shapes.GetShape(sourceConnect.FromSheet);
                                if (sourceShape == null || sourceShape.Del == BOOL.True)
                                    continue;

                                // Determine if the source shape is a decision node.
                                // This example assumes decision nodes have the universal name "Decision".
                                if (!string.IsNullOrEmpty(sourceShape.NameU) &&
                                    sourceShape.NameU.Equals("Decision", StringComparison.OrdinalIgnoreCase))
                                {
                                    // Set the line jump style of the connector to Arc (always jump).
                                    shape.SetConnectorJumpValue(ConLineJumpCodeValue.Always, ConLineJumpStyleValue.Arc);
                                }
                            }
                        }
                    }

                    // Prepare output file path (same file name, different folder).
                    string outputPath = Path.Combine(outputFolder, Path.GetFileName(filePath));

                    // Save the modified diagram in the same format as the source.
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Processed and saved: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}