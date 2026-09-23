using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file, output PNG file and the name of the layer to export
                string visioPath = "input.vsdx";
                string pngPath = "layer_output.png";
                string targetLayerName = "OverlayLayer";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // Assume we work with the first page
                Page page = diagram.Pages[0];

                // Store original visibility of each layer so we can restore later
                Dictionary<string, BOOL> originalVisibility = new Dictionary<string, BOOL>();
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    originalVisibility[layer.Name.Value] = layer.Visible.Value;
                }

                // Set only the target layer visible; hide all others
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    if (layer.Name.Value.Equals(targetLayerName, StringComparison.OrdinalIgnoreCase))
                    {
                        layer.Visible.Value = BOOL.True;
                    }
                    else
                    {
                        layer.Visible.Value = BOOL.False;
                    }
                }

                // Configure PNG export options
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                // Ensure the background is transparent (default for PNG when no background shape is drawn)
                // No additional property is required; Aspose.Diagram renders transparent PNG automatically.

                // Export the diagram (first page) as PNG
                diagram.Save(pngPath, saveOptions);

                // Restore original layer visibility
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    if (originalVisibility.TryGetValue(layer.Name.Value, out BOOL original))
                    {
                        layer.Visible.Value = original;
                    }
                }

                Console.WriteLine($"Layer \"{targetLayerName}\" exported to \"{pngPath}\" as a transparent PNG.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }