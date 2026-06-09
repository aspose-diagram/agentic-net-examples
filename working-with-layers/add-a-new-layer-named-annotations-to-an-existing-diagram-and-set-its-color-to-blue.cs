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

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the existing diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the first page (layers are page‑specific)
                Page page = diagram.Pages[0];

                // Create a new layer called "Annotations"
                Layer layer = new Layer();
                layer.Name.Value = "Annotations";

                // Set the layer color to blue (hex format)
                layer.Color.Value = "#0000FF";

                // Enable the color for the layer
                layer.IsColorChecked = BOOL.True;

                // Add the new layer to the page's layer collection
                page.PageSheet.Layers.Add(layer);

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
