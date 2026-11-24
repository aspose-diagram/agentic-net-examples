using System;
using System.IO;
using System.Linq; // Required for Contains on string[]
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page because layers are stored per page
                foreach (Page page in diagram.Pages)
                {
                    // Locate the source layer named "Draft"
                    Layer sourceLayer = null;
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        if (layer.Name != null && layer.Name.Value == "Draft")
                        {
                            sourceLayer = layer;
                            break;
                        }
                    }

                    if (sourceLayer == null)
                    {
                        // If the Draft layer does not exist on this page, skip to next page
                        continue;
                    }

                    // Create a new layer and copy properties from the source layer
                    Layer clonedLayer = new Layer();
                    clonedLayer.Name.Value = "Final";                         // Rename the cloned layer
                    clonedLayer.Visible.Value = sourceLayer.Visible.Value;   // Preserve visibility
                    clonedLayer.Print.Value = sourceLayer.Print.Value;       // Preserve print flag
                    clonedLayer.Lock.Value = sourceLayer.Lock.Value;         // Preserve lock flag
                    clonedLayer.IsColorChecked = sourceLayer.IsColorChecked; // Preserve color‑check flag
                    // clonedLayer.Color = sourceLayer.Color; // Removed: Color is read‑only

                    // Add the cloned layer to the page's layer collection
                    page.PageSheet.Layers.Add(clonedLayer);

                    // The new layer receives an index (IX) automatically
                    int newLayerIndex = clonedLayer.IX;

                    // Update shapes that belong to the source layer so they also belong to the cloned layer
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a layer membership object
                        if (shape.LayerMem == null || shape.LayerMem.LayerMember == null)
                            continue;

                        string membership = shape.LayerMem.LayerMember.Value ?? string.Empty;

                        // Determine if the shape is currently assigned to the source layer
                        bool belongsToSource = false;
                        if (!string.IsNullOrEmpty(membership))
                        {
                            string[] parts = membership.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string part in parts)
                            {
                                if (int.TryParse(part, out int ix) && ix == sourceLayer.IX)
                                {
                                    belongsToSource = true;
                                    break;
                                }
                            }
                        }

                        // If the shape is on the Draft layer, add the Final layer index
                        if (belongsToSource)
                        {
                            // Append the new layer index if it's not already present
                            if (!membership.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Contains(newLayerIndex.ToString()))
                            {
                                string newMembership = string.IsNullOrEmpty(membership)
                                    ? newLayerIndex.ToString()
                                    : membership + ";" + newLayerIndex.ToString();

                                shape.LayerMem.LayerMember.Value = newMembership;
                            }
                        }
                    }
                }

                // Save the modified diagram in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
