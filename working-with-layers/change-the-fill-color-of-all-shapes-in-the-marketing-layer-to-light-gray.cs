using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <exe> <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Load the Visio diagram.
        Diagram diagram = new Diagram(inputPath);

        // Iterate through all pages in the document.
        foreach (Page page in diagram.Pages)
        {
            // Find the index of the layer named "Marketing".
            int marketingLayerIndex = -1;
            foreach (Layer layer in page.PageSheet.Layers)
            {
                if (layer.Name.Value == "Marketing")
                {
                    marketingLayerIndex = layer.IX;
                    break;
                }
            }

            // If the layer does not exist on this page, skip to the next page.
            if (marketingLayerIndex == -1)
                continue;

            string marketingIndexString = marketingLayerIndex.ToString();

            // Update fill color for each shape that belongs to the Marketing layer.
            foreach (Shape shape in page.Shapes)
            {
                // Skip deleted shapes.
                if (shape.Del == BOOL.True)
                    continue;

                // The LayerMember property holds a semicolon‑separated list of layer indexes.
                string layerMember = shape.LayerMem.LayerMember.Value ?? string.Empty;
                string[] memberIndexes = layerMember.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                // If the shape is assigned to the Marketing layer, change its fill.
                foreach (string idx in memberIndexes)
                {
                    if (idx == marketingIndexString)
                    {
                        // Ensure solid fill pattern.
                        shape.Fill.FillPattern.Value = 1;               // Solid fill.
                        shape.Fill.FillForegnd.Value = "#D3D3D3";       // Light gray.
                        break;
                    }
                }
            }
        }

        // Save the modified diagram as VSDX.
        diagram.Save(outputPath, SaveFileFormat.Vsdx);
        Console.WriteLine($"Diagram saved to '{outputPath}'.");
    }
}
