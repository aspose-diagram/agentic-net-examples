using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Find the layer named "UI" on the current page
                    Layer uiLayer = null;
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        if (layer.Name != null && layer.Name.Value == "UI")
                        {
                            uiLayer = layer;
                            break;
                        }
                    }

                    // If the UI layer does not exist on this page, skip processing
                    if (uiLayer == null)
                        continue;

                    string uiLayerIndexString = uiLayer.IX.ToString();

                    // Iterate over all shapes on the page
                    for (int i = 0; i < page.Shapes.Count; i++)
                    {
                        Shape shape = page.Shapes[i];

                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has layer membership information
                        if (shape.LayerMem == null || shape.LayerMem.LayerMember == null)
                            continue;

                        string layerMember = shape.LayerMem.LayerMember.Value ?? string.Empty;

                        // Check if the shape belongs to the UI layer (comma‑separated list of indices)
                        string[] memberIndices = layerMember.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        bool belongsToUi = false;
                        foreach (string idx in memberIndices)
                        {
                            if (idx.Trim() == uiLayerIndexString)
                            {
                                belongsToUi = true;
                                break;
                            }
                        }

                        if (!belongsToUi)
                            continue;

                        // Apply drop shadow effect to the shape
                        shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;      // Simple drop shadow
                        shape.Fill.ShdwForegnd.Value = "#000000";                       // Shadow color: black
                        shape.Fill.ShdwForegndTrans.Value = 0.3;                        // 30% transparency
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}