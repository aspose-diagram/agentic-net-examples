using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file, target layer name, and output PNG path.
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string targetLayerName = "OverlayLayer";
        string outputPath = "layer_overlay.png";

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all layers on the current page.
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // Make only the target layer visible; hide others.
                        if (layer.Name != null && layer.Name.Value == targetLayerName)
                        {
                            layer.Visible.Value = BOOL.True;
                        }
                        else
                        {
                            layer.Visible.Value = BOOL.False;
                        }
                    }
                }

                // Configure image save options for PNG export.
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                // Export only the first page (adjust PageIndex/PageCount as needed).
                saveOptions.PageIndex = 0;
                saveOptions.PageCount = 1;

                // NOTE: Aspose.Diagram's ImageSaveOptions does not expose a TransparentBackground property.
                // The exported PNG will use the default background (typically white). This is the closest
                // achievable result with the available API.

                // Save the diagram as a PNG image.
                diagram.Save(outputPath, saveOptions);
                Console.WriteLine($"Layer \"{targetLayerName}\" exported to PNG: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console.
            Console.Error.WriteLine($"Error during export: {ex.Message}");
        }
    }
}