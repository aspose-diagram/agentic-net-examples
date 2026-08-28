using System;
using System.Collections.Generic;
using System.IO; // Required for Path and File operations
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath;
        if (args.Length > 0)
        {
            inputPath = args[0];
        }
        else
        {
            Console.Write("Enter the path to the Visio file: ");
            inputPath = Console.ReadLine();
        }

        // Guard to ensure the file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        if (string.IsNullOrWhiteSpace(inputPath))
        {
            Console.WriteLine("No file path provided. Exiting.");
            return;
        }

        // Load the diagram
        Diagram diagram;
        try
        {
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Assume processing the first page (adjust if needed)
        if (diagram.Pages.Count == 0)
        {
            Console.WriteLine("The diagram contains no pages.");
            return;
        }

        Page page = diagram.Pages[0];

        // Store original visibility of each layer
        var originalVisibility = new Dictionary<Layer, BOOL>();
        foreach (Layer layer in page.PageSheet.Layers)
        {
            originalVisibility[layer] = layer.Visible.Value;
        }

        // Iterate over layers and export each visible layer as a separate PDF
        foreach (Layer layer in page.PageSheet.Layers)
        {
            if (layer.Visible.Value != BOOL.True)
                continue; // Skip invisible layers

            // Hide all layers
            foreach (Layer l in page.PageSheet.Layers)
            {
                l.Visible.Value = BOOL.False;
            }

            // Show only the current layer
            layer.Visible.Value = BOOL.True;

            // Prepare output file name (use layer name, replace invalid path chars)
            string safeLayerName = string.Join("_", layer.Name.Value.Split(Path.GetInvalidFileNameChars()));
            string outputPath = $"{safeLayerName}.pdf";

            // Save the diagram as PDF with the current layer visible
            var pdfOptions = new PdfSaveOptions();
            try
            {
                diagram.Save(outputPath, pdfOptions);
                Console.WriteLine($"Exported layer '{layer.Name.Value}' to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to export layer '{layer.Name.Value}': {ex.Message}");
            }

            // Restore original visibility for all layers before next iteration
            foreach (Layer l in page.PageSheet.Layers)
            {
                l.Visible.Value = originalVisibility[l];
            }
        }

        // Cleanup
        diagram.Dispose();
    }
}