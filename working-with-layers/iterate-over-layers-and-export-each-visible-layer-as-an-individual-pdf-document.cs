using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard against missing file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Load the diagram inside a using block as required
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page in the diagram
                int pageIndex = 0;
                foreach (Page page in diagram.Pages)
                {
                    // Collect layers of the current page into a list for safe iteration
                    var pageLayers = new System.Collections.Generic.List<Layer>();
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        pageLayers.Add(layer);
                    }

                    // Process each visible layer
                    foreach (Layer targetLayer in pageLayers)
                    {
                        // Check if the layer is visible
                        if (targetLayer.Visible.Value != BOOL.True)
                            continue;

                        // Preserve original visibility states of all layers on this page
                        var originalVisibilities = new System.Collections.Generic.List<(Layer layer, BOOL visibility)>();
                        foreach (Layer layer in pageLayers)
                        {
                            originalVisibilities.Add((layer, layer.Visible.Value));
                        }

                        try
                        {
                            // Hide all layers
                            foreach (Layer layer in pageLayers)
                            {
                                layer.Visible.Value = BOOL.False;
                            }

                            // Show only the target layer
                            targetLayer.Visible.Value = BOOL.True;

                            // Prepare output file name (sanitize layer name)
                            string rawLayerName = targetLayer.Name.Value ?? "UnnamedLayer";
                            foreach (char c in Path.GetInvalidFileNameChars())
                            {
                                rawLayerName = rawLayerName.Replace(c, '_');
                            }

                            string outputFileName = $"{Path.GetFileNameWithoutExtension(inputPath)}_Page{pageIndex}_Layer_{rawLayerName}.pdf";
                            string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", outputFileName);

                            // Configure PDF save options
                            PdfSaveOptions pdfOptions = new PdfSaveOptions();

                            // Export the diagram (current page visibility settings will affect the output)
                            diagram.Save(outputPath, pdfOptions);

                            Console.WriteLine($"Exported layer '{targetLayer.Name.Value}' to PDF: {outputPath}");
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Error exporting layer '{targetLayer.Name.Value}': {ex.Message}");
                        }
                        finally
                        {
                            // Restore original visibility states
                            foreach (var (layer, visibility) in originalVisibilities)
                            {
                                layer.Visible.Value = visibility;
                            }
                        }
                    }

                    pageIndex++;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to process diagram: {ex.Message}");
        }
    }
}