using System;
using System.Collections.Generic;
using System.Linq;
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

                // Work with the first page (adjust if needed)
                Page page = diagram.Pages[0];

                // Locate the "Draft" and "Review" layers
                Layer draftLayer = null;
                Layer reviewLayer = null;

                foreach (Layer layer in page.PageSheet.Layers)
                {
                    if (layer.Name.Value.Equals("Draft", StringComparison.OrdinalIgnoreCase))
                        draftLayer = layer;
                    else if (layer.Name.Value.Equals("Review", StringComparison.OrdinalIgnoreCase))
                        reviewLayer = layer;
                }

                // If the Draft layer does not exist, create a new one to hold merged shapes
                if (draftLayer == null)
                {
                    draftLayer = new Layer();
                    draftLayer.Name.Value = "Draft";
                    draftLayer.Visible.Value = BOOL.True;
                    page.PageSheet.Layers.Add(draftLayer);
                }

                // Store layer indexes as strings for later comparison
                string draftIdx = draftLayer.IX.ToString();
                string reviewIdx = reviewLayer?.IX.ToString();

                // Iterate shapes in order and reassign layer membership
                foreach (Shape shape in page.Shapes)
                {
                    // Current layer membership (semicolon‑separated list of indexes)
                    string member = shape.LayerMem.LayerMember.Value ?? string.Empty;

                    // Split into a mutable list
                    List<string> parts = member
                        .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                        .ToList();

                    // If the shape belongs to the Review layer, move it to Draft
                    if (reviewLayer != null && parts.Contains(reviewIdx))
                    {
                        // Remove Review index
                        parts.Remove(reviewIdx);

                        // Ensure Draft index is present
                        if (!parts.Contains(draftIdx))
                            parts.Add(draftIdx);

                        // Update the shape's layer membership
                        shape.LayerMem.LayerMember.Value = string.Join(";", parts);
                    }
                }

                // Hide the Review layer after merging (optional: could also remove it if API allowed)
                if (reviewLayer != null)
                    reviewLayer.Visible.Value = BOOL.False;

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }