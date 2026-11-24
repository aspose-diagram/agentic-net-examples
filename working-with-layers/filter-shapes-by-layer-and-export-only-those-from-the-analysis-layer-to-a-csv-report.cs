using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputCsvPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required by the API)
            using (Diagram diagram = new Diagram(inputPath))
            {
                const string targetLayerName = "Analysis";

                // Iterate over each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Find the index (IX) of the target layer on this page
                    int targetLayerIndex = -1;
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // Layer.Name is a Str2Value wrapper; compare its Value property
                        if (layer.Name != null && layer.Name.Value == targetLayerName)
                        {
                            targetLayerIndex = layer.IX;
                            break;
                        }
                    }

                    // If the target layer is not present, delete all shapes on this page
                    if (targetLayerIndex == -1)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            shape.Del = BOOL.True;
                        }
                        continue;
                    }

                    // Process each shape on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes already marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the layer membership string (e.g., "0;2;5")
                        string layerMember = shape.LayerMem?.LayerMember?.Value;

                        bool belongsToTarget = false;

                        if (!string.IsNullOrEmpty(layerMember))
                        {
                            // Split the semicolon‑separated list and check for the target index
                            string[] parts = layerMember.Split(';');
                            foreach (string part in parts)
                            {
                                if (int.TryParse(part, out int idx) && idx == targetLayerIndex)
                                {
                                    belongsToTarget = true;
                                    break;
                                }
                            }
                        }

                        // If the shape is not on the target layer, mark it as deleted
                        if (!belongsToTarget)
                        {
                            shape.Del = BOOL.True;
                        }
                    }
                }

                // Export the filtered diagram to CSV format
                diagram.Save(outputPath, SaveFileFormat.Csv);
                Console.WriteLine($"CSV report generated at: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}