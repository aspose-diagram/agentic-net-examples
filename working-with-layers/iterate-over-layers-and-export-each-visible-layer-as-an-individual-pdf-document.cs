using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";

                // Output directory for PDFs
                string outputDir = "LayerPdfs";
                Directory.CreateDirectory(outputDir);

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Assume processing the first page (index 0)
                if (diagram.Pages.Count == 0)
                {
                    Console.WriteLine("The diagram contains no pages.");
                    return;
                }

                Page page = diagram.Pages[0];

                // Capture original visibility of all layers
                List<BOOL> originalVisibilities = new List<BOOL>();
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    originalVisibilities.Add(layer.Visible.Value);
                }

                // Iterate over each layer
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    // Process only layers that are originally visible
                    if (layer.Visible.Value != BOOL.True)
                        continue;

                    // Hide all layers
                    foreach (Layer l in page.PageSheet.Layers)
                    {
                        l.Visible.Value = BOOL.False;
                    }

                    // Show the current layer
                    layer.Visible.Value = BOOL.True;

                    // Prepare output file name (use layer index to avoid illegal characters)
                    string outputPath = Path.Combine(outputDir, $"Layer_{layer.IX}.pdf");

                    // Export the diagram as PDF with the current layer visibility
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();
                    diagram.Save(outputPath, pdfOptions);

                    Console.WriteLine($"Exported visible layer '{layer.Name.Value}' to '{outputPath}'.");

                    // Restore original visibility for all layers
                    int idx = 0;
                    foreach (Layer l in page.PageSheet.Layers)
                    {
                        l.Visible.Value = originalVisibilities[idx];
                        idx++;
                    }
                }

                // Cleanup
                diagram.Dispose();
                Console.WriteLine("Layer export completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }