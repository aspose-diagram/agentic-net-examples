using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output_protected.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Name of the target layer
            string targetLayerName = "MyLayer";

            // Find the layer index (IX) for the specified layer name
            int targetLayerIndex = -1;
            foreach (Layer layer in diagram.Pages[0].PageSheet.Layers)
            {
                if (layer.Name.Value == targetLayerName)
                {
                    targetLayerIndex = layer.IX;
                    break;
                }
            }

            if (targetLayerIndex == -1)
            {
                Console.WriteLine($"Layer \"{targetLayerName}\" not found.");
                return;
            }

            string targetLayerIndexStr = targetLayerIndex.ToString();

            // Iterate all pages and shapes, applying protection to shapes on the target layer
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has layer membership information
                    if (shape.LayerMem != null && shape.LayerMem.LayerMember != null)
                    {
                        string memberValue = shape.LayerMem.LayerMember.Value;
                        // Check if the shape belongs to the target layer (semicolon‑separated list)
                        string[] layers = memberValue.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (string idx in layers)
                        {
                            if (idx == targetLayerIndexStr)
                            {
                                // Apply protection flags
                                shape.Protection.LockMoveX.Value = BOOL.True;
                                shape.Protection.LockMoveY.Value = BOOL.True;
                                shape.Protection.LockWidth.Value = BOOL.True;
                                shape.Protection.LockHeight.Value = BOOL.True;
                                shape.Protection.LockRotate.Value = BOOL.True;
                                shape.Protection.LockVtxEdit.Value = BOOL.True;
                                // Additional locks can be set here as needed
                                break;
                            }
                        }
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Shape protection applied and diagram saved.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
