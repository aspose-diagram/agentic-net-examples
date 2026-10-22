using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
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
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Convert 2 points to inches (Visio stores line weight in inches).
                double lineWeightInInches = 2.0 / 72.0;

                // Iterate through each page because layers are stored per page.
                foreach (Page page in diagram.Pages)
                {
                    // Find the layer named "Infrastructure" in the current page.
                    Layer targetLayer = null;
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        if (layer.Name.Value == "Infrastructure")
                        {
                            targetLayer = layer;
                            break;
                        }
                    }

                    // If the layer does not exist on this page, skip to the next page.
                    if (targetLayer == null)
                        continue;

                    // The zero‑based index of the target layer.
                    int layerIndex = targetLayer.IX;

                    // Iterate over all shapes on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has layer membership information.
                        if (shape.LayerMem == null || shape.LayerMem.LayerMember == null)
                            continue;

                        string layerMember = shape.LayerMem.LayerMember.Value;
                        if (string.IsNullOrEmpty(layerMember))
                            continue;

                        // Check if the shape belongs to the target layer.
                        // The membership string is a semicolon‑separated list of indexes.
                        string[] members = layerMember.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        bool belongsToTarget = false;
                        foreach (string m in members)
                        {
                            if (m == layerIndex.ToString())
                            {
                                belongsToTarget = true;
                                break;
                            }
                        }

                        if (belongsToTarget)
                        {
                            // Set the line weight to 2 points (converted to inches).
                            shape.Line.LineWeight.Value = lineWeightInInches;
                        }
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}