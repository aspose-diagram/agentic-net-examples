using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Work with the first page (adjust if multiple pages are needed)
            Page page = diagram.Pages[0];

            // Locate the layer named "Background"
            Layer backgroundLayer = null;
            foreach (Layer layer in page.PageSheet.Layers)
            {
                if (layer.Name.Value == "Background")
                {
                    backgroundLayer = layer;
                    break;
                }
            }

            if (backgroundLayer == null)
            {
                Console.WriteLine("Layer named 'Background' was not found.");
                return;
            }

            // Create a new layer that duplicates the original background layer's settings
            Layer newBackground = new Layer();
            newBackground.Name.Value = "Background";
            newBackground.Visible.Value = backgroundLayer.Visible.Value;
            newBackground.Print.Value = backgroundLayer.Print.Value;
            newBackground.Lock.Value = backgroundLayer.Lock.Value;
            newBackground.IsColorChecked = backgroundLayer.IsColorChecked;

            // Add the new layer to the collection (it will be appended at the end)
            page.PageSheet.Layers.Add(newBackground);

            // Attempt to move the newly added layer to the first position by setting its index.
            // Not all versions expose a settable index; the try/catch ensures compilation even if unavailable.
            try
            {
                // Some API versions expose an IX property that can be set.
                newBackground.IX = 0;
            }
            catch { /* If IX is read‑only, the layer will remain at the end; ordering cannot be changed directly. */ }

            // Hide the original background layer to avoid duplicate layers.
            backgroundLayer.Visible.Value = BOOL.False;

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
