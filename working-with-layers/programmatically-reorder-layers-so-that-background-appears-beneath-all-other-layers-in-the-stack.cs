using System;
using System.IO;
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

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the document.
                foreach (Page page in diagram.Pages)
                {
                    // Access the layer collection of the current page.
                    LayerCollection layers = page.PageSheet.Layers;

                    // Locate the layer named "Background".
                    Layer backgroundLayer = null;
                    foreach (Layer layer in layers)
                    {
                        if (layer.Name.Value == "Background")
                        {
                            backgroundLayer = layer;
                            break;
                        }
                    }

                    // If there is no background layer on this page, skip it.
                    if (backgroundLayer == null)
                        continue;

                    // ------------------------------------------------------------
                    // Create a copy of the background layer to place at the bottom.
                    // ------------------------------------------------------------
                    Layer newLayer = new Layer();
                    newLayer.Name.Value = backgroundLayer.Name.Value;
                    newLayer.Visible.Value = backgroundLayer.Visible.Value;
                    newLayer.Print.Value = backgroundLayer.Print.Value;
                    newLayer.Lock.Value = backgroundLayer.Lock.Value;
                    newLayer.IsColorChecked = backgroundLayer.IsColorChecked;
                    newLayer.Color.Value = backgroundLayer.Color.Value;

                    // Add the new layer to the collection.
                    layers.Add(newLayer);

                    // Attempt to set the new layer's index to 0 (bottom of the stack).
                    try
                    {
                        newLayer.IX = 0;
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Failed to set new layer index: {ex.Message}");
                    }

                    // ------------------------------------------------------------
                    // Move shapes that belong to the original background layer
                    // to the newly created layer.
                    // ------------------------------------------------------------
                    int originalIX = backgroundLayer.IX;
                    int newIX = newLayer.IX;

                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        var layerMember = shape.LayerMem?.LayerMember?.Value;
                        if (string.IsNullOrEmpty(layerMember))
                            continue;

                        string[] parts = layerMember.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        bool changed = false;
                        for (int i = 0; i < parts.Length; i++)
                        {
                            if (parts[i].Trim() == originalIX.ToString())
                            {
                                parts[i] = newIX.ToString();
                                changed = true;
                            }
                        }

                        if (changed)
                        {
                            // Update the shape's layer membership.
                            shape.LayerMem.LayerMember.Value = string.Join(",", parts);
                            // Ensure the shape stays behind other content.
                            shape.SendToBack();
                        }
                    }

                    // Hide the original background layer so it no longer affects rendering.
                    backgroundLayer.Visible.Value = BOOL.False;
                }

                // Save the modified diagram to the specified output file.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}