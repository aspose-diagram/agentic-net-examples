using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
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

            // Process each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Locate the "Draft", "Review", and target "DraftReview" layers.
                Layer draftLayer = null;
                Layer reviewLayer = null;
                Layer mergedLayer = null;

                // Iterate over the page's layer collection (non‑generic legacy collection).
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    string layerName = layer.Name.Value;
                    if (layerName == "Draft")
                        draftLayer = layer;
                    else if (layerName == "Review")
                        reviewLayer = layer;
                    else if (layerName == "DraftReview")
                        mergedLayer = layer;
                }

                // If the merged layer does not exist, create it.
                if (mergedLayer == null)
                {
                    mergedLayer = new Layer();
                    mergedLayer.Name.Value = "DraftReview";
                    mergedLayer.Visible.Value = BOOL.True;
                    page.PageSheet.Layers.Add(mergedLayer);
                }

                // Store the index strings for quick comparison.
                string draftIndex = draftLayer != null ? draftLayer.IX.ToString() : null;
                string reviewIndex = reviewLayer != null ? reviewLayer.IX.ToString() : null;
                string mergedIndex = mergedLayer.IX.ToString();

                // Iterate over all shapes on the current page preserving order.
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve current layer membership string.
                    string membership = shape.LayerMem.LayerMember.Value ?? string.Empty;

                    // Split the membership into individual layer indices.
                    string[] parts = membership.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                    // Build a new membership list, removing Draft and Review indices.
                    List<string> newMembers = new List<string>();
                    bool belongsToOldLayer = false;

                    foreach (string part in parts)
                    {
                        string trimmed = part.Trim();
                        if (draftIndex != null && trimmed == draftIndex)
                        {
                            belongsToOldLayer = true;
                            continue; // Skip Draft layer.
                        }
                        if (reviewIndex != null && trimmed == reviewIndex)
                        {
                            belongsToOldLayer = true;
                            continue; // Skip Review layer.
                        }
                        // Keep other layer indices.
                        newMembers.Add(trimmed);
                    }

                    // If the shape was originally on Draft or Review, add it to the merged layer.
                    if (belongsToOldLayer)
                    {
                        // Ensure the merged layer index is not already present.
                        bool alreadyHasMerged = false;
                        foreach (string m in newMembers)
                        {
                            if (m == mergedIndex)
                            {
                                alreadyHasMerged = true;
                                break;
                            }
                        }
                        if (!alreadyHasMerged)
                        {
                            newMembers.Add(mergedIndex);
                        }
                    }

                    // Re‑assign the updated membership string.
                    shape.LayerMem.LayerMember.Value = string.Join(";", newMembers);
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}