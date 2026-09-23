using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        // Verify the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Preserve original visibility of all layers on this page
                var originalVisibilities = new Dictionary<Layer, BOOL>();
                foreach (Layer l in page.PageSheet.Layers)
                {
                    originalVisibilities[l] = l.Visible.Value;
                }

                // Process each visible layer individually
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    // Skip layers that are not visible in the original document
                    if (layer.Visible.Value != BOOL.True)
                        continue;

                    // Hide all layers on the current page
                    foreach (Layer l in page.PageSheet.Layers)
                    {
                        l.Visible.Value = BOOL.False;
                    }

                    // Show only the current layer
                    layer.Visible.Value = BOOL.True;

                    // Prepare a safe file name for the layer
                    string safeLayerName = SanitizeFileName(layer.Name.Value);
                    string outputDir = Path.Combine("output");
                    Directory.CreateDirectory(outputDir);
                    // Use page.ID (unique numeric identifier) instead of non‑existent Index property
                    string outputPath = Path.Combine(outputDir, $"{safeLayerName}_Page{page.ID}.pdf");

                    // Configure PDF save options to export only the current page
                    PdfSaveOptions pdfOptions = new PdfSaveOptions
                    {
                        PageIndex = page.ID // Export the current page only
                    };

                    // Save the diagram as PDF; only the current layer will be visible
                    diagram.Save(outputPath, pdfOptions);
                }

                // Restore original layer visibility for the page
                foreach (var kvp in originalVisibilities)
                {
                    kvp.Key.Visible.Value = kvp.Value;
                }
            }
        }
        catch (Exception ex)
        {
            // Write any runtime errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Helper to replace invalid filename characters with underscore
    private static string SanitizeFileName(string name)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(c, '_');
        }
        return name;
    }
}