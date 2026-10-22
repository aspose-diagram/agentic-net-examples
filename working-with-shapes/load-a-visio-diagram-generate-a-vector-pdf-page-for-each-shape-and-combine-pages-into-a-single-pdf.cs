using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPdfPath = args[1];

        // Guard: check if input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Create a temporary directory to store per‑shape PDFs
        string tempDir = Path.Combine(Path.GetTempPath(), "ShapePdfTemp_" + Guid.NewGuid().ToString("N"));
        try
        {
            Directory.CreateDirectory(tempDir);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to create temporary directory: {ex.Message}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Collect all generated PDF file paths
            List<string> shapePdfFiles = new List<string>();

            // Iterate over each page
            for (int pageIdx = 0; pageIdx < diagram.Pages.Count; pageIdx++)
            {
                Page page = diagram.Pages[pageIdx];

                // Iterate over each shape on the page
                for (int shapeIdx = 0; shapeIdx < page.Shapes.Count; shapeIdx++)
                {
                    Shape shape = page.Shapes[shapeIdx];

                    // Build a unique file name for the shape PDF
                    string shapePdfPath = Path.Combine(
                        tempDir,
                        $"page{pageIdx + 1}_shape{shape.ID}.pdf");

                    try
                    {
                        // Convert the shape to a PDF file (vector output)
                        shape.ToPdf(shapePdfPath);
                        shapePdfFiles.Add(shapePdfPath);
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Error converting shape ID {shape.ID} on page {pageIdx + 1}: {ex.Message}");
                    }
                }
            }

            // Combine all shape PDFs into a single PDF document using Aspose.Pdf
            // Fully qualified types are used to avoid namespace ambiguity
            Aspose.Pdf.Document combinedPdf = new Aspose.Pdf.Document();

            foreach (string pdfFile in shapePdfFiles)
            {
                try
                {
                    // Load the individual shape PDF
                    Aspose.Pdf.Document part = new Aspose.Pdf.Document(pdfFile);

                    // Append each page from the part to the combined document
                    // Aspose.Pdf pages are 1‑based indexed
                    for (int i = 1; i <= part.Pages.Count; i++)
                    {
                        combinedPdf.Pages.Add(part.Pages[i]);
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error merging PDF file '{pdfFile}': {ex.Message}");
                }
            }

            // Save the combined PDF to the requested output path
            try
            {
                combinedPdf.Save(outputPdfPath);
                Console.WriteLine($"Combined PDF saved to: {outputPdfPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to save combined PDF: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
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
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to clean up temporary directory: {ex.Message}");
            }
        }
    }
}