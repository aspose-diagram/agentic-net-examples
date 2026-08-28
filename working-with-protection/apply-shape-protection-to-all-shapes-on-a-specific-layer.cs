using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Name of the layer whose shapes should be protected
            string targetLayerName = "MyLayer";

            // Find the index (IX) of the target layer (assumes same index on all pages)
            int layerIndex = -1;
            foreach (Layer layer in diagram.Pages[0].PageSheet.Layers)
            {
                if (layer.Name.Value == targetLayerName)
                {
                    layerIndex = layer.IX;
                    break;
                }
            }

            if (layerIndex == -1)
            {
                Console.WriteLine($"Layer '{targetLayerName}' not found.");
                return;
            }

            string layerIndexStr = layerIndex.ToString();

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Get the layer membership string (semicolon‑separated indexes)
                    string members = shape.LayerMem.LayerMember.Value;
                    if (string.IsNullOrEmpty(members))
                        continue;

                    // Check if the shape belongs to the target layer
                    string[] parts = members.Split(';');
                    foreach (string part in parts)
                    {
                        if (part == layerIndexStr)
                        {
                            // Apply protection flags
                            shape.Protection.LockMoveX.Value = BOOL.True;
                            shape.Protection.LockMoveY.Value = BOOL.True;
                            shape.Protection.LockWidth.Value = BOOL.True;
                            shape.Protection.LockHeight.Value = BOOL.True;
                            shape.Protection.LockRotate.Value = BOOL.True;
                            shape.Protection.LockVtxEdit.Value = BOOL.True;
                            // Additional locks can be set here if required
                            break;
                        }
                    }
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Shape protection applied and diagram saved.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
