using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file, target layer name, and output PNG path.
            string inputPath = "input.vsdx";
            string targetLayerName = "OverlayLayer";
            string outputPath = "layer_overlay.png";

            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Assume we work with the first page.
            Page page = diagram.Pages[0];

            // Hide all layers first.
            foreach (Layer layer in page.PageSheet.Layers)
            {
                layer.Visible.Value = BOOL.False;
            }

            // Find the target layer and make it visible.
            bool layerFound = false;
            foreach (Layer layer in page.PageSheet.Layers)
            {
                if (layer.Name.Value == targetLayerName)
                {
                    layer.Visible.Value = BOOL.True;
                    layerFound = true;
                    break;
                }
            }

            if (!layerFound)
            {
                throw new Exception($"Layer '{targetLayerName}' not found in the diagram.");
            }

            // Configure PNG export options.
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            // Export only the first page (index 0).
            saveOptions.PageIndex = 0;
            // Ensure background is transparent (default for PNG in Aspose.Diagram).
            // Additional options can be set here if needed, e.g., resolution.
            saveOptions.Resolution = 300; // DPI

            // Save the diagram as a PNG with only the target layer visible.
            diagram.Save(outputPath, saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
