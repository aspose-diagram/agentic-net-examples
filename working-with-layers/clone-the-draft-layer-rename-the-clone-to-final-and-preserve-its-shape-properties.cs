using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (can be overridden via command line arguments)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the source Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                bool draftLayerFoundInAnyPage = false;

                // Iterate through each page because layers are stored per page sheet
                foreach (Page page in diagram.Pages)
                {
                    // Locate the original "Draft" layer on this page
                    Layer originalLayer = null;
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        if (layer.Name.Value == "Draft")
                        {
                            originalLayer = layer;
                            break;
                        }
                    }

                    // If the page does not contain the "Draft" layer, skip it
                    if (originalLayer == null)
                    {
                        continue;
                    }

                    draftLayerFoundInAnyPage = true;

                    // Clone the original layer's properties into a new layer named "Final"
                    Layer clonedLayer = new Layer();
                    clonedLayer.Name.Value = "Final";                                 // New name
                    clonedLayer.Visible.Value = originalLayer.Visible.Value;          // Preserve visibility
                    clonedLayer.Print.Value = originalLayer.Print.Value;              // Preserve print flag
                    clonedLayer.Lock.Value = originalLayer.Lock.Value;                // Preserve lock flag
                    clonedLayer.IsColorChecked = originalLayer.IsColorChecked;       // Preserve color check (BOOL, no .Value)
                    clonedLayer.Color.Value = originalLayer.Color.Value;              // Preserve color

                    // Add the cloned layer to the page's layer collection
                    page.PageSheet.Layers.Add(clonedLayer);

                    // Retrieve the index (IX) of the original and cloned layers as strings
                    string originalLayerIndex = originalLayer.IX.ToString();
                    string clonedLayerIndex = clonedLayer.IX.ToString();

                    // Iterate through all shapes on the page to copy layer membership
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a LayerMem object (should always be present)
                        if (shape.LayerMem == null || shape.LayerMem.LayerMember == null)
                        {
                            continue;
                        }

                        // Get current layer membership string (semicolon‑separated indexes)
                        string membership = shape.LayerMem.LayerMember.Value ?? string.Empty;

                        // Determine if the shape belongs to the original "Draft" layer
                        bool belongsToDraft = false;
                        if (!string.IsNullOrEmpty(membership))
                        {
                            string[] parts = membership.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string part in parts)
                            {
                                if (part == originalLayerIndex)
                                {
                                    belongsToDraft = true;
                                    break;
                                }
                            }
                        }

                        // If the shape is part of the Draft layer, add the Final layer index
                        if (belongsToDraft)
                        {
                            // Avoid duplicate addition of the cloned layer index
                            bool alreadyHasCloned = false;
                            if (!string.IsNullOrEmpty(membership))
                            {
                                string[] parts = membership.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                                foreach (string part in parts)
                                {
                                    if (part == clonedLayerIndex)
                                    {
                                        alreadyHasCloned = true;
                                        break;
                                    }
                                }
                            }

                            if (!alreadyHasCloned)
                            {
                                string newMembership = string.IsNullOrEmpty(membership)
                                    ? clonedLayerIndex
                                    : membership + ";" + clonedLayerIndex;
                                shape.LayerMem.LayerMember.Value = newMembership;
                            }
                        }
                    }
                }

                if (!draftLayerFoundInAnyPage)
                {
                    Console.Error.WriteLine("Error: Layer named \"Draft\" was not found in any page of the diagram.");
                    return;
                }

                // Save the modified diagram to the output file
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}