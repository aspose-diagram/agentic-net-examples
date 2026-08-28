using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "merged.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Names of the layers to merge
            const string draftLayerName = "Draft";
            const string reviewLayerName = "Review";
            const string mergedLayerName = "DraftReview";

            // Assume all pages share the same layer collection; use the first page to manage layers
            Page firstPage = diagram.Pages[0];
            LayerCollection layers = firstPage.PageSheet.Layers;

            // Find existing Draft and Review layers
            Layer draftLayer = null;
            Layer reviewLayer = null;
            foreach (Layer layer in layers)
            {
                if (layer.Name.Value.Equals(draftLayerName, StringComparison.OrdinalIgnoreCase))
                    draftLayer = layer;
                else if (layer.Name.Value.Equals(reviewLayerName, StringComparison.OrdinalIgnoreCase))
                    reviewLayer = layer;
            }

            if (draftLayer == null && reviewLayer == null)
            {
                Console.WriteLine("Neither Draft nor Review layers were found. No changes made.");
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                return;
            }

            // Create (or reuse) the merged layer
            Layer mergedLayer = null;
            foreach (Layer layer in layers)
            {
                if (layer.Name.Value.Equals(mergedLayerName, StringComparison.OrdinalIgnoreCase))
                {
                    mergedLayer = layer;
                    break;
                }
            }
            if (mergedLayer == null)
            {
                mergedLayer = new Layer();
                mergedLayer.Name.Value = mergedLayerName;
                mergedLayer.Visible.Value = BOOL.True;
                layers.Add(mergedLayer);
            }

            // Get the indexes (IX) of the layers as strings
            string draftIndex = draftLayer?.IX.ToString();
            string reviewIndex = reviewLayer?.IX.ToString();
            string mergedIndex = mergedLayer.IX.ToString();

            // Iterate all pages and shapes to reassign layer memberships
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Get current layer membership string (e.g., "0;2")
                    string membership = shape.LayerMem.LayerMember.Value;
                    if (string.IsNullOrEmpty(membership))
                        continue;

                    // Split into individual indexes
                    string[] parts = membership.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    bool needsUpdate = false;
                    var newParts = new System.Collections.Generic.List<string>();

                    foreach (string part in parts)
                    {
                        // Keep indexes that are not Draft or Review
                        if (part == draftIndex || part == reviewIndex)
                        {
                            needsUpdate = true;
                            continue;
                        }
                        newParts.Add(part);
                    }

                    // If the shape was on Draft or Review, add the merged layer index
                    if (needsUpdate)
                    {
                        if (!newParts.Contains(mergedIndex))
                            newParts.Add(mergedIndex);

                        // Rebuild the membership string
                        shape.LayerMem.LayerMember.Value = string.Join(";", newParts);
                    }
                }
            }

            // Optionally hide the original Draft and Review layers
            if (draftLayer != null)
                draftLayer.Visible.Value = BOOL.False;
            if (reviewLayer != null)
                reviewLayer.Visible.Value = BOOL.False;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Layers merged and diagram saved to " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
