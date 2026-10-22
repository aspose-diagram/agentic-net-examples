using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // List to keep track of temporary PDF files generated for each shape.
        List<string> tempPdfFiles = new List<string>();

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page to preserve page order.
            for (int pageIdx = 0; pageIdx < diagram.Pages.Count; pageIdx++)
            {
                // Retrieve the current page.
                Page page = diagram.Pages[pageIdx];

                // Retrieve the layer collection for the current page.
                LayerCollection layers = page.PageSheet.Layers;

                // Process layers in their original order.
                for (int layerIdx = 0; layerIdx < layers.Count; layerIdx++)
                {
                    Layer layer = layers[layerIdx];
                    string layerIndexString = layer.IX.ToString();

                    // Iterate over all shapes on the page.
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Determine if the shape belongs to the current layer.
                        // The LayerMember property holds a comma‑separated list of layer indexes.
                        string layerMember = shape.LayerMem?.LayerMember?.Value;
                        if (string.IsNullOrEmpty(layerMember))
                            continue; // Shape is not assigned to any layer.

                        // Split the member string and check for the current layer index.
                        string[] memberIndices = layerMember.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        bool belongsToLayer = false;
                        foreach (string idx in memberIndices)
                        {
                            if (idx.Trim() == layerIndexString)
                            {
                                belongsToLayer = true;
                                break;
                            }
                        }

                        if (!belongsToLayer)
                            continue; // Skip shapes not in this layer.

                        // Generate a temporary PDF file for the shape.
                        string tempPdfPath = Path.Combine(
                            Path.GetTempPath(),
                            $"shape_page{pageIdx}_layer{layerIdx}_id{shape.ID}.pdf");

                        // Convert the shape to a PDF page.
                        shape.ToPdf(tempPdfPath);

                        // Record the temporary file for later merging.
                        tempPdfFiles.Add(tempPdfPath);
                    }
                }
            }

            // Merge all temporary PDFs into a single document using Aspose.Pdf (fully qualified).
            var finalPdf = new Aspose.Pdf.Document();

            foreach (string tempFile in tempPdfFiles)
            {
                // Load each temporary PDF.
                var tempDoc = new Aspose.Pdf.Document(tempFile);

                // Append each page from the temporary document to the final document.
                foreach (Aspose.Pdf.Page page in tempDoc.Pages)
                {
                    finalPdf.Pages.Add(page);
                }
            }

            // Save the merged PDF to the requested output path.
            finalPdf.Save(outputPath);
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up temporary PDF files.
            foreach (string tempFile in tempPdfFiles)
            {
                try
                {
                    if (File.Exists(tempFile))
                        File.Delete(tempFile);
                }
                catch
                {
                    // Suppress any cleanup errors.
                }
            }
        }
    }
}