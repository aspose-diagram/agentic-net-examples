using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Define input file path
        string inputPath = "input.vsdx";
        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        Diagram diagram;
        try
        {
            // Load the Visio diagram
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Assume we work with the first page
        Page page = diagram.Pages[0];

        // Find the indexes of the "Draft" and "Review" layers
        int draftIndex = -1;
        int reviewIndex = -1;
        foreach (Layer layer in page.PageSheet.Layers)
        {
            if (layer.Name.Value == "Draft")
                draftIndex = layer.IX;
            else if (layer.Name.Value == "Review")
                reviewIndex = layer.IX;
        }

        if (draftIndex == -1 && reviewIndex == -1)
        {
            Console.WriteLine("Neither 'Draft' nor 'Review' layers were found.");
            return;
        }

        // Create a new merged layer
        Layer mergedLayer = new Layer();
        mergedLayer.Name.Value = "DraftReview";
        // Set visibility via the .Value property (Visible is read‑only)
        mergedLayer.Visible.Value = BOOL.True;
        mergedLayer.IsColorChecked = BOOL.True;
        page.PageSheet.Layers.Add(mergedLayer);
        int mergedIndex = mergedLayer.IX;

        // Update each shape's layer membership
        foreach (Shape shape in page.Shapes)
        {
            // Get current layer membership string (e.g., "0;2")
            string member = shape.LayerMem.LayerMember.Value;
            if (string.IsNullOrEmpty(member))
                continue;

            // Split into individual indexes
            string[] parts = member.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> newParts = new List<string>();
            bool needsUpdate = false;

            foreach (string part in parts)
            {
                if (int.TryParse(part, out int idx))
                {
                    if (idx == draftIndex || idx == reviewIndex)
                    {
                        // Replace Draft/Review index with merged layer index
                        if (!newParts.Contains(mergedIndex.ToString()))
                            newParts.Add(mergedIndex.ToString());
                        needsUpdate = true;
                    }
                    else
                    {
                        // Preserve other layer indexes
                        if (!newParts.Contains(part))
                            newParts.Add(part);
                    }
                }
            }

            if (needsUpdate)
            {
                // Reassign the updated layer membership string
                shape.LayerMem.LayerMember.Value = string.Join(";", newParts);
            }
        }

        // Optionally hide the original layers
        foreach (Layer layer in page.PageSheet.Layers)
        {
            if (layer.Name.Value == "Draft" || layer.Name.Value == "Review")
            {
                // Hide layer via the .Value property
                layer.Visible.Value = BOOL.False;
            }
        }

        try
        {
            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            Console.WriteLine("Layers merged and diagram saved as 'output.vsdx'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }
}