using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

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

        // Guard: check input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create a temporary directory for per‑shape PDFs
        string tempDir = Path.Combine(Path.GetTempPath(), "ShapePdfTemp_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        try
        {
            // Load the Visio diagram inside a using block
            using (Diagram diagram = new Diagram(inputPath))
            {
                // List to hold generated PDF file paths
                List<string> shapePdfFiles = new List<string>();

                // Iterate over each page and each shape to export the shape as a PDF page
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over shapes in the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Build a unique file name for the shape PDF
                        string shapePdfPath = Path.Combine(
                            tempDir,
                            $"Page_{page.ID}_Shape_{shape.ID}.pdf");

                        // Export the shape to PDF using the Shape.ToPdf method (vector output)
                        shape.ToPdf(shapePdfPath);

                        // Store the generated file path for later combination
                        shapePdfFiles.Add(shapePdfPath);
                    }
                }

                // Combine all per‑shape PDFs into a single PDF document using Aspose.Pdf
                var combinedPdf = new Aspose.Pdf.Document();

                foreach (string pdfFile in shapePdfFiles)
                {
                    // Load the individual PDF
                    var singlePdf = new Aspose.Pdf.Document(pdfFile);

                    // Add each page of the single PDF to the combined document
                    // Here we assume each shape PDF contains exactly one page
                    if (singlePdf.Pages.Count > 0)
                    {
                        combinedPdf.Pages.Add(singlePdf.Pages[1]);
                    }
                }

                // Save the combined PDF to the requested output path
                combinedPdf.Save(outputPdfPath);
                Console.WriteLine($"Combined PDF saved to: {outputPdfPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine("An error occurred: " + ex.Message);
        }
        finally
        {
            // Clean up temporary files and directory
            try
            {
                if (Directory.Exists(tempDir))
                {
                    Directory.Delete(tempDir, true);
                }
            }
            catch
            {
                // Suppress any cleanup exceptions
            }
        }
    }
}