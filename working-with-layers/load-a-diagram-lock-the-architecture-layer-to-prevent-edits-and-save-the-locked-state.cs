using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output diagram path
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <exe> <inputPath> <outputPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Load the Visio diagram
        Diagram diagram = new Diagram(inputPath);

        // Assume the layer exists on the first page
        Page page = diagram.Pages[0];

        // Find the layer named "Architecture"
        int architectureLayerIndex = -1;
        foreach (Layer layer in page.PageSheet.Layers)
        {
            if (layer.Name.Value == "Architecture")
            {
                architectureLayerIndex = layer.IX; // IX is the zero‑based index of the layer
                break;
            }
        }

        if (architectureLayerIndex == -1)
        {
            Console.WriteLine("Layer 'Architecture' not found.");
            return;
        }

        // Lock all shapes that belong to the Architecture layer
        foreach (Shape shape in page.Shapes)
        {
            string layerMember = shape.LayerMem.LayerMember.Value; // e.g., "0;2;5"
            if (string.IsNullOrEmpty(layerMember))
                continue;

            // Split the semicolon‑separated list of layer indexes
            string[] parts = layerMember.Split(';');
            foreach (string part in parts)
            {
                if (int.TryParse(part, out int idx) && idx == architectureLayerIndex)
                {
                    // Apply full protection to the shape
                    shape.Protection.LockMoveX.Value = BOOL.True;
                    shape.Protection.LockMoveY.Value = BOOL.True;
                    shape.Protection.LockWidth.Value = BOOL.True;
                    shape.Protection.LockHeight.Value = BOOL.True;
                    shape.Protection.LockRotate.Value = BOOL.True;
                    shape.Protection.LockVtxEdit.Value = BOOL.True;
                    break; // No need to check other parts for this shape
                }
            }
        }

        // Save the diagram with the locked layer state
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
        Console.WriteLine($"Diagram saved to '{outputPath}' with 'Architecture' layer locked.");
    }
}
