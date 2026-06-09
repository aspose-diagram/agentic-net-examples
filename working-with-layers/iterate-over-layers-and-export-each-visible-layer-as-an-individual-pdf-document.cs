using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file path (replace with actual path or pass via args)
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Preserve original visibility states of layers
                var originalVisibility = new System.Collections.Generic.Dictionary<long, BOOL>();
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    originalVisibility[layer.IX] = layer.Visible.Value;
                }

                // Iterate over each layer on the current page
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    // Process only layers that are originally visible
                    if (layer.Visible.Value == BOOL.True)
                    {
                        // Hide all layers except the current one
                        foreach (Layer otherLayer in page.PageSheet.Layers)
                        {
                            otherLayer.Visible.Value = (otherLayer == layer) ? BOOL.True : BOOL.False;
                        }

                        // Prepare output PDF file name using the layer name
                        string safeLayerName = layer.Name.Value.Replace(" ", "_");
                        string outputFileName = $"{Path.GetFileNameWithoutExtension(inputPath)}_Layer_{safeLayerName}.pdf";

                        // Configure PDF save options
                        PdfSaveOptions pdfOptions = new PdfSaveOptions();

                        // Save the diagram as PDF with only the current layer visible
                        diagram.Save(outputFileName, pdfOptions);
                    }
                }

                // Restore original visibility for all layers
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    if (originalVisibility.TryGetValue(layer.IX, out BOOL originalValue))
                    {
                        layer.Visible.Value = originalValue;
                    }
                }
            }

            // Dispose diagram resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
