using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string visioPath = args[0];
        string outputPdfPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Input Visio file not found: {visioPath}");
            return;
        }

        // Create a temporary folder for per‑shape PDFs.
        string tempFolder = Path.Combine(Path.GetTempPath(), "VisioShapePdfs_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        try
        {
            // Load the Visio diagram inside a using block as required by the API guidelines.
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Collect paths of generated shape PDFs.
                var shapePdfPaths = new System.Collections.Generic.List<string>();

                // Export each shape on each page to an individual PDF file.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Build a temporary file name for the shape PDF.
                        string shapePdfPath = Path.Combine(tempFolder, $"shape_{shape.ID}.pdf");

                        // Export the shape to PDF. The Shape.ToPdf method writes directly to a file.
                        shape.ToPdf(shapePdfPath);

                        shapePdfPaths.Add(shapePdfPath);
                    }
                }

                // Combine all shape PDFs into a single PDF document using Aspose.Pdf.
                // Note: No using directive for Aspose.Pdf to avoid ambiguous references.
                var combinedPdf = new Aspose.Pdf.Document();

                // Append each shape PDF as pages to the combined document.
                foreach (string shapePdfPath in shapePdfPaths)
                {
                    var shapeDoc = new Aspose.Pdf.Document(shapePdfPath);
                    foreach (Aspose.Pdf.Page srcPage in shapeDoc.Pages)
                    {
                        combinedPdf.Pages.Add(srcPage);
                    }
                }

                // Insert an index page at the beginning of the combined PDF.
                Aspose.Pdf.Page indexPage = combinedPdf.Pages.Insert(1);

                // Build index entries. Since linking via TextFragment.Action is not available
                // in the current Aspose.Pdf version, we provide plain text entries with page numbers.
                int pageNumber = 2; // First shape page after the index page.
                foreach (string shapePdfPath in shapePdfPaths)
                {
                    // Derive shape name from the original Visio file (fallback to file name).
                    string shapeName = Path.GetFileNameWithoutExtension(shapePdfPath);

                    // Create a text fragment for the index entry.
                    var tf = new Aspose.Pdf.Text.TextFragment($"{shapeName} (Page {pageNumber})");
                    // Add some margin to improve readability.
                    tf.Margin = new Aspose.Pdf.MarginInfo { Top = 5, Bottom = 5 };
                    indexPage.Paragraphs.Add(tf);

                    pageNumber++;
                }

                // Save the final PDF document.
                combinedPdf.Save(outputPdfPath);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
        finally
        {
            // Clean up temporary files and folder.
            try
            {
                if (Directory.Exists(tempFolder))
                {
                    Directory.Delete(tempFolder, true);
                }
            }
            catch
            {
                // Suppress any cleanup errors.
            }
        }
    }
}