using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string visioPath = "input.vsdx";
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        // Name of the layer to export
        string targetLayerName = "OverlayLayer";

        // Output PNG file path
        string outputPngPath = "layer_overlay.png";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Assume we work with the first page; adjust if needed
            Page page = diagram.Pages[0];

            // Iterate through all layers on the page and set visibility
            foreach (Layer layer in page.PageSheet.Layers)
            {
                if (layer.Name.Value.Equals(targetLayerName, StringComparison.OrdinalIgnoreCase))
                {
                    // Make the target layer visible
                    layer.Visible.Value = BOOL.True;
                }
                else
                {
                    // Hide all other layers
                    layer.Visible.Value = BOOL.False;
                }
            }

            // Configure image save options for PNG
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
            // Export only the first page (index 0)
            saveOptions.PageIndex = 0;
            // Do not export hidden pages (if any)
            saveOptions.ExportHiddenPage = false;

            // Save the diagram as a PNG; only the visible layer will appear
            diagram.Save(outputPngPath, saveOptions);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
            return;
        }

        Console.WriteLine("Layer exported successfully to: " + outputPngPath);
    }
}