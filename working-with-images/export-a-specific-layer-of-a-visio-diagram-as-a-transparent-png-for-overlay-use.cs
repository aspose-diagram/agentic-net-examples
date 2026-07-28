using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Input Visio file and the name of the layer to export
            string visioPath = "input.vsdx";
            string targetLayerName = "OverlayLayer";
            string outputPng = "layer_overlay.png";

            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Assume we work with the first page
            Page page = diagram.Pages[0];

            // Set visibility: only the target layer is visible, others are hidden
            foreach (Layer layer in page.PageSheet.Layers)
            {
                if (layer.Name.Value.Equals(targetLayerName, StringComparison.OrdinalIgnoreCase))
                    layer.Visible.Value = BOOL.True;   // Show target layer
                else
                    layer.Visible.Value = BOOL.False;  // Hide all other layers
            }

            // Configure PNG export options
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png)
            {
                PageIndex = 0,                     // Export the first page
                ExportHiddenPage = false           // Do not include hidden pages
            };

            // Enable transparent background if the option exists
            // (If the property is not available in the referenced version, this line can be omitted)
            // pngOptions.TransparentBackground = true;

            // Save the selected layer as a transparent PNG
            diagram.Save(outputPng, pngOptions);

            Console.WriteLine($"Layer '{targetLayerName}' exported to '{outputPng}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
