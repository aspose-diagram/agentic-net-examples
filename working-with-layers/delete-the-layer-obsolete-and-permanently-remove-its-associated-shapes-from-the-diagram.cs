using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    int obsoleteLayerIndex = -1;
                    Layer obsoleteLayer = null;

                    // Locate the layer named "Obsolete"
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        if (layer.Name.Value == "Obsolete")
                        {
                            obsoleteLayer = layer;
                            obsoleteLayerIndex = layer.IX;
                            // Hide the layer (no direct removal API)
                            layer.Visible.Value = BOOL.False;
                            break;
                        }
                    }

                    // If the layer was not found on this page, continue to next page
                    if (obsoleteLayerIndex == -1)
                        continue;

                    // Collect shapes that belong to the obsolete layer
                    List<Shape> shapesToRemove = new List<Shape>();
                    foreach (Shape shape in page.Shapes)
                    {
                        string layerMember = shape.LayerMem.LayerMember.Value;
                        if (string.IsNullOrEmpty(layerMember))
                            continue;

                        // LayerMember contains semicolon‑separated layer indexes
                        string[] members = layerMember.Split(';');
                        foreach (string member in members)
                        {
                            if (int.TryParse(member, out int idx) && idx == obsoleteLayerIndex)
                            {
                                shapesToRemove.Add(shape);
                                break;
                            }
                        }
                    }

                    // Remove the collected shapes from the page
                    foreach (Shape shape in shapesToRemove)
                    {
                        page.Shapes.Remove(shape);
                    }

                    // Note: Aspose.Diagram does not provide a direct method to delete a Layer object.
                    // The layer has been hidden above, and its shapes have been permanently removed.
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
