using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputVisioFile]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length > 1
            ? args[1]
            : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", Path.GetFileNameWithoutExtension(inputPath) + "_protected.vsdx");

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Find the index of the layer named "ReadOnly"
                int readOnlyLayerIndex = -1;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        if (layer.Name != null && layer.Name.Value == "ReadOnly")
                        {
                            // IX is an int, not a cell with .Value
                            readOnlyLayerIndex = layer.IX;
                            break;
                        }
                    }
                    if (readOnlyLayerIndex != -1)
                        break;
                }

                if (readOnlyLayerIndex == -1)
                {
                    Console.Error.WriteLine("Layer named \"ReadOnly\" was not found in the diagram.");
                    return;
                }

                // Apply protection to all shapes that belong to the "ReadOnly" layer
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve the layer membership string (semicolon or comma separated list of layer indices)
                        string layerMember = shape.LayerMem?.LayerMember?.Value;
                        if (string.IsNullOrEmpty(layerMember))
                            continue;

                        // Split using both ';' and ',' as delimiters
                        string[] parts = layerMember.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string part in parts)
                        {
                            if (int.TryParse(part.Trim(), out int idx) && idx == readOnlyLayerIndex)
                            {
                                // Apply comprehensive protection flags
                                shape.Protection.LockMoveX.Value = BOOL.True;
                                shape.Protection.LockMoveY.Value = BOOL.True;
                                shape.Protection.LockWidth.Value = BOOL.True;
                                shape.Protection.LockHeight.Value = BOOL.True;
                                shape.Protection.LockRotate.Value = BOOL.True;
                                shape.Protection.LockAspect.Value = BOOL.True;
                                shape.Protection.LockBegin.Value = BOOL.True;
                                shape.Protection.LockEnd.Value = BOOL.True;
                                shape.Protection.LockCalcWH.Value = BOOL.True;
                                shape.Protection.LockCrop.Value = BOOL.True;
                                shape.Protection.LockCustProp.Value = BOOL.True;
                                shape.Protection.LockDelete.Value = BOOL.True;
                                shape.Protection.LockFormat.Value = BOOL.True;
                                shape.Protection.LockFromGroupFormat.Value = BOOL.True;
                                shape.Protection.LockGroup.Value = BOOL.True;
                                shape.Protection.LockSelect.Value = BOOL.True;
                                shape.Protection.LockTextEdit.Value = BOOL.True;
                                shape.Protection.LockThemeColors.Value = BOOL.True;
                                shape.Protection.LockThemeEffects.Value = BOOL.True;
                                shape.Protection.LockVtxEdit.Value = BOOL.True;

                                // No need to check other indices for this shape
                                break;
                            }
                        }
                    }
                }

                // Save the modified diagram with protections applied
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved with protections applied: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}