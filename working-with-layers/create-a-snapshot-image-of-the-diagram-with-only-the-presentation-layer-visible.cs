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

            // Path to the source Visio diagram
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Name of the layer to keep visible
            string targetLayerName = "Presentation";

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all layers on the page
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    // Set visibility: only the target layer is visible
                    if (layer.Name.Value.Equals(targetLayerName, StringComparison.OrdinalIgnoreCase))
                    {
                        layer.Visible.Value = BOOL.True;
                    }
                    else
                    {
                        layer.Visible.Value = BOOL.False;
                    }
                }
            }

            // Configure image export options (PNG format)
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);

            // Export the diagram as an image snapshot
            string outputPath = "snapshot.png";
            diagram.Save(outputPath, saveOptions);

            Console.WriteLine($"Snapshot saved to '{outputPath}' with only the '{targetLayerName}' layer visible.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
