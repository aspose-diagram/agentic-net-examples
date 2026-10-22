using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input file, target layer name, optional output file
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <layerName> [outputVisioFile]");
            return;
        }

        string inputPath = args[0];
        string targetLayerName = args[1];
        string outputPath = args.Length >= 3 ? args[2] : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "output.vsdx");

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Find the layer with the specified name on the current page
                    Layer targetLayer = null;
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        if (layer.Name.Value.Equals(targetLayerName, StringComparison.OrdinalIgnoreCase))
                        {
                            targetLayer = layer;
                            break;
                        }
                    }

                    // If the layer does not exist on this page, skip to next page
                    if (targetLayer == null)
                        continue;

                    // The layer index (IX) is stored as a long; convert to string for comparison
                    string layerIndexString = targetLayer.IX.ToString();

                    // Iterate over all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has layer membership information
                        if (shape.LayerMem == null || shape.LayerMem.LayerMember == null)
                            continue;

                        // Retrieve the comma‑separated list of layer indices the shape belongs to
                        string memberString = shape.LayerMem.LayerMember.Value ?? string.Empty;
                        string[] memberIndices = memberString.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                        // Check if the shape belongs to the target layer
                        bool belongsToTargetLayer = false;
                        foreach (string idx in memberIndices)
                        {
                            if (idx.Trim() == layerIndexString)
                            {
                                belongsToTargetLayer = true;
                                break;
                            }
                        }

                        if (!belongsToTargetLayer)
                            continue;

                        // Apply protection to the shape – set all supported lock properties to TRUE
                        shape.Protection.LockAspect.Value = BOOL.True;
                        shape.Protection.LockBegin.Value = BOOL.True;
                        shape.Protection.LockCalcWH.Value = BOOL.True;
                        shape.Protection.LockCrop.Value = BOOL.True;
                        shape.Protection.LockCustProp.Value = BOOL.True;
                        shape.Protection.LockDelete.Value = BOOL.True;
                        shape.Protection.LockEnd.Value = BOOL.True;
                        shape.Protection.LockFormat.Value = BOOL.True;
                        shape.Protection.LockFromGroupFormat.Value = BOOL.True;
                        shape.Protection.LockGroup.Value = BOOL.True;
                        shape.Protection.LockHeight.Value = BOOL.True;
                        shape.Protection.LockMoveX.Value = BOOL.True;
                        shape.Protection.LockMoveY.Value = BOOL.True;
                        shape.Protection.LockRotate.Value = BOOL.True;
                        shape.Protection.LockSelect.Value = BOOL.True;
                        shape.Protection.LockTextEdit.Value = BOOL.True;
                        shape.Protection.LockThemeColors.Value = BOOL.True;
                        shape.Protection.LockThemeEffects.Value = BOOL.True;
                        shape.Protection.LockVtxEdit.Value = BOOL.True;
                        shape.Protection.LockWidth.Value = BOOL.True;
                    }
                }

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}