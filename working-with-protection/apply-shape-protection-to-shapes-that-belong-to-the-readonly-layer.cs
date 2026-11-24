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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
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
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Find the layer named "ReadOnly" across all pages (layers are stored per page).
            // We'll assume the layer exists on the first page; if not found, report and exit.
            Layer readOnlyLayer = null;
            foreach (Page page in diagram.Pages)
            {
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    if (layer.Name != null && layer.Name.Value == "ReadOnly")
                    {
                        readOnlyLayer = layer;
                        break;
                    }
                }
                if (readOnlyLayer != null) break;
            }

            if (readOnlyLayer == null)
            {
                Console.Error.WriteLine("Layer named \"ReadOnly\" was not found in the diagram.");
                return;
            }

            // The layer index (IX) is used in the shape's LayerMember string.
            string targetLayerIndex = readOnlyLayer.IX.ToString();

            // Iterate through all pages and shapes, applying protection to shapes that belong to the "ReadOnly" layer.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has layer membership information.
                    if (shape.LayerMem == null || shape.LayerMem.LayerMember == null)
                        continue;

                    string memberValue = shape.LayerMem.LayerMember.Value ?? string.Empty;
                    // Split the semicolon‑separated list of layer indices.
                    string[] memberIndices = memberValue.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                    bool isInReadOnlyLayer = false;
                    foreach (string idx in memberIndices)
                    {
                        if (idx.Trim() == targetLayerIndex)
                        {
                            isInReadOnlyLayer = true;
                            break;
                        }
                    }

                    if (!isInReadOnlyLayer)
                        continue; // Shape is not part of the target layer.

                    // Apply full protection to the shape.
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

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}