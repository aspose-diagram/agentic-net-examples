using System.IO;
using System;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the source Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Work with the first page (adjust if needed)
            Page page = diagram.Pages[0];

            // Locate the layer named "Draft"
            Layer draftLayer = null;
            foreach (Layer layer in page.PageSheet.Layers)
            {
                if (layer.Name.Value == "Draft")
                {
                    draftLayer = layer;
                    break;
                }
            }

            if (draftLayer == null)
            {
                Console.WriteLine("Layer 'Draft' not found.");
                return;
            }

            // Clone the Draft layer into a new layer named "Final"
            Layer finalLayer = new Layer();
            finalLayer.Name.Value = "Final";
            finalLayer.Visible.Value = draftLayer.Visible.Value;
            finalLayer.Print.Value = draftLayer.Print.Value;
            finalLayer.IsColorChecked = draftLayer.IsColorChecked; // BOOL enum assignment

            // Add the new layer to the page's layer collection
            page.PageSheet.Layers.Add(finalLayer);

            // Update shape layer memberships:
            // Any shape that belongs to the Draft layer will also be assigned to the Final layer
            string draftIndex = draftLayer.IX.ToString();
            string finalIndex = finalLayer.IX.ToString();

            foreach (Shape shape in page.Shapes)
            {
                // Ensure the LayerMember string is not null
                string membership = shape.LayerMem.LayerMember?.Value ?? string.Empty;

                // Check if the shape is a member of the Draft layer
                var members = membership.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                if (members.Contains(draftIndex))
                {
                    // Add the Final layer index if it's not already present
                    if (!members.Contains(finalIndex))
                    {
                        string newMembership = string.IsNullOrEmpty(membership)
                            ? finalIndex
                            : membership + ";" + finalIndex;
                        shape.LayerMem.LayerMember.Value = newMembership;
                    }
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Layer cloned successfully. Saved to '{outputPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
