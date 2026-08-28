using System;
using System.IO;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        // Define input and output file paths
        string inputPath = "input.vsdx";
        string outputPath = "output.vsdx";

        // Guard: ensure the source Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the source Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Assume the operation is performed on the first page.
            // Adjust if you need to process multiple pages.
            Page page = diagram.Pages[0];

            // Locate the layer named "Draft"
            Layer draftLayer = null;
            foreach (Layer layer in page.PageSheet.Layers)
            {
                // Compare the layer's name value with the target name
                if (layer.Name.Value == "Draft")
                {
                    draftLayer = layer;
                    break;
                }
            }

            if (draftLayer == null)
            {
                throw new Exception("Layer named 'Draft' was not found.");
            }

            // Create a new layer and copy the properties from the Draft layer
            Layer finalLayer = new Layer();
            finalLayer.Name.Value = "Final";
            finalLayer.Visible.Value = draftLayer.Visible.Value;
            finalLayer.Print.Value = draftLayer.Print.Value;
            finalLayer.Status.Value = draftLayer.Status.Value;

            // Some Visio versions expose IsColorChecked; copy if available
            try
            {
                // IsColorChecked is a BOOL enum, not a BoolValue, so assign directly
                finalLayer.IsColorChecked = draftLayer.IsColorChecked;
            }
            catch (Exception)
            {
                // Property not available – ignore
            }

            // Add the new layer to the page's layer collection
            page.PageSheet.Layers.Add(finalLayer);

            // Store the numeric indexes of the original and cloned layers
            int draftIndex = draftLayer.IX;
            int finalIndex = finalLayer.IX;

            // Iterate all pages and shapes to duplicate layer membership
            foreach (Page pg in diagram.Pages)
            {
                foreach (Shape shape in pg.Shapes)
                {
                    // Retrieve current layer membership string (e.g., "0;2")
                    string membership = shape.LayerMem.LayerMember.Value;

                    if (string.IsNullOrEmpty(membership))
                    {
                        continue; // Shape is not assigned to any layer
                    }

                    // Split into individual indexes
                    string[] parts = membership.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    bool hasDraft = false;
                    bool hasFinal = false;

                    foreach (string part in parts)
                    {
                        if (part == draftIndex.ToString())
                            hasDraft = true;
                        if (part == finalIndex.ToString())
                            hasFinal = true;
                    }

                    // If the shape belongs to the Draft layer, ensure it also belongs to the Final layer
                    if (hasDraft && !hasFinal)
                    {
                        // Append the final layer index
                        membership = membership + ";" + finalIndex;
                        shape.LayerMem.LayerMember.Value = membership;
                    }
                }
            }

            // Guard: ensure the output directory exists
            string outputDir = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
            {
                Directory.CreateDirectory(outputDir);
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}