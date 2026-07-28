using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Assume processing the first page (adjust if needed)
            Page page = diagram.Pages[0];

            // Locate the 'Draft' and 'Review' layers
            Layer draftLayer = null;
            Layer reviewLayer = null;
            foreach (Layer layer in page.PageSheet.Layers)
            {
                if (layer.Name.Value == "Draft")
                    draftLayer = layer;
                else if (layer.Name.Value == "Review")
                    reviewLayer = layer;
            }

            if (draftLayer == null && reviewLayer == null)
            {
                Console.WriteLine("Neither 'Draft' nor 'Review' layers were found.");
                return;
            }

            // Create a new merged layer
            Layer mergedLayer = new Layer();
            mergedLayer.Name.Value = "DraftReview";
            mergedLayer.Visible.Value = BOOL.True;
            mergedLayer.IsColorChecked = BOOL.True;
            page.PageSheet.Layers.Add(mergedLayer);

            // Prepare index strings for comparison
            string draftIdx = draftLayer?.IX.ToString();
            string reviewIdx = reviewLayer?.IX.ToString();
            string mergedIdx = mergedLayer.IX.ToString();

            // Update shape layer memberships
            foreach (Shape shape in page.Shapes)
            {
                // Ensure the shape has a layer membership string
                string memberValue = shape.LayerMem.LayerMember.Value;
                if (string.IsNullOrEmpty(memberValue))
                    continue;

                // Split existing memberships
                var members = new HashSet<string>(memberValue.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries));

                bool containsTarget = false;
                if (draftIdx != null && members.Contains(draftIdx))
                {
                    members.Remove(draftIdx);
                    containsTarget = true;
                }
                if (reviewIdx != null && members.Contains(reviewIdx))
                {
                    members.Remove(reviewIdx);
                    containsTarget = true;
                }

                // If the shape belonged to either layer, add the merged layer index
                if (containsTarget)
                {
                    members.Add(mergedIdx);
                    shape.LayerMem.LayerMember.Value = string.Join(";", members);
                }
            }

            // Optionally hide the original layers
            if (draftLayer != null)
                draftLayer.Visible.Value = BOOL.False;
            if (reviewLayer != null)
                reviewLayer.Visible.Value = BOOL.False;

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            Console.WriteLine("Layers merged and diagram saved as 'output.vsdx'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
