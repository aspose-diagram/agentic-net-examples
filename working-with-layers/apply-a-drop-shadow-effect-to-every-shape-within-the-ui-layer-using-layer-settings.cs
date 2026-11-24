using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block (Diagram implements IDisposable)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Locate the layer named "UI"
                Layer uiLayer = null;
                foreach (Page page in diagram.Pages)
                {
                    // Ensure the page has a PageSheet (rarely null)
                    if (page.PageSheet == null) continue;

                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // Compare the layer name via its Str2Value wrapper
                        if (layer.Name != null && layer.Name.Value == "UI")
                        {
                            uiLayer = layer;
                            break;
                        }
                    }

                    if (uiLayer != null) break;
                }

                if (uiLayer == null)
                {
                    Console.Error.WriteLine("Layer named 'UI' was not found in the diagram.");
                    return;
                }

                // Store the UI layer index as a string for later comparison
                string uiLayerIndex = uiLayer.IX.ToString();

                // Iterate through all pages and shapes to apply the shadow
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True) continue;

                        // Ensure the shape has layer membership information
                        if (shape.LayerMem == null || shape.LayerMem.LayerMember == null) continue;

                        // Split the semicolon‑separated list of layer indices
                        var memberIndices = shape.LayerMem.LayerMember.Value
                            .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(s => s.Trim());

                        // Process only shapes that belong to the UI layer
                        if (!memberIndices.Contains(uiLayerIndex)) continue;

                        // Apply a simple drop shadow
                        shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;   // Enable shadow
                        shape.Fill.ShdwForegnd.Value = "#000000";                    // Shadow color (black)
                        shape.Fill.ShdwForegndTrans.Value = 0.3;                     // 30% transparency

                        // Set default shadow offsets (adjust as needed)
                        shape.Fill.ShapeShdwOffsetX.Value = 0.05; // Horizontal offset
                        shape.Fill.ShapeShdwOffsetY.Value = 0.05; // Vertical offset
                    }
                }

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}