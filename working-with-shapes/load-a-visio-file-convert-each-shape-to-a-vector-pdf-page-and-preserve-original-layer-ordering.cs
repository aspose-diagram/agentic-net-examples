using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPdfPath = args[1];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a temporary directory for per‑shape PDFs
        string tempDir = Path.Combine(Path.GetTempPath(), "ShapePdfExport_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // List to keep track of generated PDF file paths in correct order
            List<string> pdfPages = new List<string>();

            // Iterate each page
            foreach (Page page in diagram.Pages)
            {
                // Iterate layers in the order they appear
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    string layerIndexStr = layer.IX.ToString();

                    // Iterate shapes in the page's shape collection
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Determine if the shape belongs to the current layer
                        if (shape.LayerMem != null && shape.LayerMem.LayerMember != null)
                        {
                            string memberValue = shape.LayerMem.LayerMember.Value ?? string.Empty;
                            string[] members = memberValue.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                            bool belongsToLayer = false;
                            foreach (string m in members)
                            {
                                if (m.Trim() == layerIndexStr)
                                {
                                    belongsToLayer = true;
                                    break;
                                }
                            }

                            if (!belongsToLayer)
                                continue; // Skip shapes not in this layer
                        }
                        else
                        {
                            // Shape has no layer membership; skip it
                            continue;
                        }

                        // Export the shape to a temporary PDF file
                        string shapePdfPath = Path.Combine(tempDir, $"shape_{shape.ID}_{Guid.NewGuid().ToString("N")}.pdf");
                        try
                        {
                            // Shape.ToPdf writes a vector PDF of the shape
                            shape.ToPdf(shapePdfPath);
                            pdfPages.Add(shapePdfPath);
                        }
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine($"Failed to export shape ID {shape.ID} to PDF: {ex.Message}");
                        }
                    }
                }
            }

            // Combine individual PDFs into a single document using Aspose.Pdf (fully qualified)
            Aspose.Pdf.Document finalPdf = new Aspose.Pdf.Document();

            foreach (string pagePath in pdfPages)
            {
                try
                {
                    // Load the temporary PDF
                    Aspose.Pdf.Document tempDoc = new Aspose.Pdf.Document(pagePath);
                    // Append each page from the temporary document to the final document
                    foreach (Aspose.Pdf.Page tempPage in tempDoc.Pages)
                    {
                        finalPdf.Pages.Add(tempPage);
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to merge PDF page '{pagePath}': {ex.Message}");
                }
            }

            // Save the combined PDF
            finalPdf.Save(outputPdfPath);
            Console.WriteLine($"Combined PDF saved to: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            // Clean up temporary files
            try
            {
                if (Directory.Exists(tempDir))
                {
                    Directory.Delete(tempDir, true);
                }
            }
            catch
            {
                // Suppress any cleanup errors
            }
        }
    }
}