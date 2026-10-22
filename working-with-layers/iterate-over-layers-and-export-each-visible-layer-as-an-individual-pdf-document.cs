using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputDirectory]");
            return;
        }

        string inputPath = args[0];
        string outputDir = args.Length >= 2 ? args[1] : Path.GetDirectoryName(inputPath) ?? Directory.GetCurrentDirectory();

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Ensure output directory exists
        if (!Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory '{outputDir}': {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Access the layer collection via the page's PageSheet
                    LayerCollection layers = page.PageSheet.Layers;

                    // Store original visibility of all layers for later restoration
                    BOOL[] originalVisibilities = new BOOL[layers.Count];
                    for (int i = 0; i < layers.Count; i++)
                    {
                        originalVisibilities[i] = layers[i].Visible.Value;
                    }

                    // Iterate over each layer
                    for (int layerIdx = 0; layerIdx < layers.Count; layerIdx++)
                    {
                        Layer layer = layers[layerIdx];

                        // Process only layers that are originally visible
                        if (layer.Visible.Value == BOOL.True)
                        {
                            // Hide all layers
                            for (int j = 0; j < layers.Count; j++)
                            {
                                layers[j].Visible.Value = BOOL.False;
                            }

                            // Show only the current layer
                            layer.Visible.Value = BOOL.True;

                            // Prepare a safe file name using page index and layer name
                            string safeLayerName = SanitizeFileName(layer.Name.Value);
                            string outputFileName = $"Page{pageIndex + 1}_Layer_{safeLayerName}.pdf";
                            string outputPath = Path.Combine(outputDir, outputFileName);

                            // Configure PDF save options
                            PdfSaveOptions pdfOptions = new PdfSaveOptions();

                            // Export the diagram (current page with current layer visibility) to PDF
                            diagram.Save(outputPath, pdfOptions);

                            Console.WriteLine($"Exported visible layer '{layer.Name.Value}' to '{outputPath}'");
                        }
                    }

                    // Restore original visibility states for the next iteration
                    for (int i = 0; i < layers.Count; i++)
                    {
                        layers[i].Visible.Value = originalVisibilities[i];
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred during processing: {ex.Message}");
        }
    }

    // Helper method to replace invalid filename characters
    private static string SanitizeFileName(string name)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(c, '_');
        }
        return string.IsNullOrWhiteSpace(name) ? "UnnamedLayer" : name;
    }
}