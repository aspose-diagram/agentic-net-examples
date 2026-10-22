using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "MergedLayers.vsdx");

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Locate the "Draft" and "Review" layers on the current page
                Layer draftLayer = null;
                Layer reviewLayer = null;

                foreach (Layer layer in page.PageSheet.Layers)
                {
                    if (layer.Name.Value == "Draft")
                        draftLayer = layer;
                    else if (layer.Name.Value == "Review")
                        reviewLayer = layer;
                }

                // If neither layer exists, continue to next page
                if (draftLayer == null && reviewLayer == null)
                    continue;

                // Determine the target layer (existing or new)
                Layer targetLayer = null;

                if (draftLayer != null && reviewLayer != null)
                {
                    // Both layers exist – create a new merged layer
                    targetLayer = new Layer();
                    targetLayer.Name.Value = "DraftReview";
                    targetLayer.Visible.Value = BOOL.True;
                    page.PageSheet.Layers.Add(targetLayer);
                }
                else if (draftLayer != null)
                {
                    // Only Draft exists – rename it to the merged name
                    draftLayer.Name.Value = "DraftReview";
                    targetLayer = draftLayer;
                }
                else // only Review exists
                {
                    reviewLayer.Name.Value = "DraftReview";
                    targetLayer = reviewLayer;
                }

                // Cache the index string of the target layer for assignment
                string targetLayerIndex = targetLayer.IX.ToString();

                // Helper function to check if a shape belongs to a given layer index
                bool ShapeBelongsToLayer(Shape shape, int layerIx)
                {
                    string memberValue = shape.LayerMem.LayerMember.Value;
                    if (string.IsNullOrEmpty(memberValue))
                        return false;

                    string[] parts = memberValue.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string part in parts)
                    {
                        if (int.TryParse(part.Trim(), out int ix) && ix == layerIx)
                            return true;
                    }
                    return false;
                }

                // Preserve shape order by iterating through the Shapes collection sequentially
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape shape = page.Shapes[i];

                    bool belongsToDraft = draftLayer != null && ShapeBelongsToLayer(shape, draftLayer.IX);
                    bool belongsToReview = reviewLayer != null && ShapeBelongsToLayer(shape, reviewLayer.IX);

                    if (belongsToDraft || belongsToReview)
                    {
                        // Assign the shape to the merged layer (replace existing layer membership)
                        shape.LayerMem.LayerMember.Value = targetLayerIndex;
                    }
                }

                // Remove the original layers if they were both present and a new layer was created
                if (draftLayer != null && reviewLayer != null && targetLayer != null && targetLayer.Name.Value == "DraftReview")
                {
                    // Removing layers may shift indices; perform removal after shape reassignment
                    page.PageSheet.Layers.Remove(draftLayer);
                    page.PageSheet.Layers.Remove(reviewLayer);
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}