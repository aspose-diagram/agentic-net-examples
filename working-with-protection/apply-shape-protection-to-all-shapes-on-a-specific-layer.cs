using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Prompt user for input Visio file path
        Console.Write("Enter the path to the Visio file: ");
        string inputPath = Console.ReadLine();

        // Guard: ensure the file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Prompt user for the target layer name
        Console.Write("Enter the name of the layer to protect its shapes: ");
        string targetLayerName = Console.ReadLine();

        // Guard: ensure a layer name was provided
        if (string.IsNullOrWhiteSpace(targetLayerName))
        {
            Console.Error.WriteLine("Error: Layer name cannot be empty.");
            return;
        }

        // Define output path (original name with suffix)
        string outputPath = Path.Combine(
            Path.GetDirectoryName(inputPath) ?? "",
            Path.GetFileNameWithoutExtension(inputPath) + "_protected.vsdx");

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

                    // The layer index (IX) is used in the shape's LayerMember string
                    string targetLayerIndex = targetLayer.IX.ToString();

                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape has a LayerMember value
                        if (shape.LayerMem == null || shape.LayerMem.LayerMember == null)
                            continue;

                        string layerMember = shape.LayerMem.LayerMember.Value;
                        if (string.IsNullOrEmpty(layerMember))
                            continue;

                        // Determine if the shape belongs to the target layer
                        string[] memberIndices = layerMember.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        bool belongsToTarget = false;
                        foreach (string idx in memberIndices)
                        {
                            if (idx.Trim() == targetLayerIndex)
                            {
                                belongsToTarget = true;
                                break;
                            }
                        }

                        if (!belongsToTarget)
                            continue;

                        // Apply protection to all supported properties
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

                // Save the modified diagram to the output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved with protected shapes to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}