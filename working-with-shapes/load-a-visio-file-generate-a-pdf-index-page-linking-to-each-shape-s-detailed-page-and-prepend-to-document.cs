using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for PDF save options if needed

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string visioPath = args.Length > 0 ? args[0] : "input.vsdx";
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        // Output PDF file path (second argument or default)
        string outputPdfPath = args.Length > 1 ? args[1] : "output.pdf";

        // Temporary folder for per‑shape PDFs
        string tempFolder = Path.Combine(Path.GetTempPath(), "VisioShapePdfs_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Create the final PDF document (fully qualified Aspose.Pdf types)
            Aspose.Pdf.Document finalPdf = new Aspose.Pdf.Document();

            // Add a blank page that will become the index page (pages are 1‑based)
            finalPdf.Pages.Add();
            Aspose.Pdf.Page indexPage = finalPdf.Pages[1];

            // List to keep track of shape descriptions and their final page numbers
            var shapeIndex = new System.Collections.Generic.List<(string Description, int PageNumber)>();

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True) continue;

                    // Build a simple description for the shape
                    string shapeDesc = !string.IsNullOrWhiteSpace(shape.NameU) ? shape.NameU : $"Shape_{shape.ID}";

                    // Export the shape to a temporary PDF file
                    string shapePdfPath = Path.Combine(tempFolder, $"shape_{shape.ID}.pdf");
                    shape.ToPdf(shapePdfPath);

                    // Load the temporary PDF and append its first page to the final document
                    Aspose.Pdf.Document shapePdf = new Aspose.Pdf.Document(shapePdfPath);
                    if (shapePdf.Pages.Count > 0)
                    {
                        // Append the page (Aspose.Pdf pages are 1‑based)
                        finalPdf.Pages.Add(shapePdf.Pages[1]);

                        // Record the page number (index page is 1, so first shape page is 2)
                        int pageNumber = finalPdf.Pages.Count;
                        shapeIndex.Add((shapeDesc, pageNumber));
                    }

                    // Delete the temporary file to keep the folder clean
                    File.Delete(shapePdfPath);
                }
            }

            // Add entries to the index page (no clickable links to avoid unavailable Action property)
            const double startX = 50;   // Horizontal start position (points)
            double currentY = 800;      // Vertical start position (points)
            const double lineHeight = 20;

            foreach (var entry in shapeIndex)
            {
                // Create a text fragment for the index entry
                Aspose.Pdf.Text.TextFragment tf = new Aspose.Pdf.Text.TextFragment($"{entry.Description} (Page {entry.PageNumber})");
                tf.TextState.FontSize = 12; // Reasonable font size
                tf.TextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("Arial"); // Fallback font

                // Position the fragment on the page
                tf.Position = new Aspose.Pdf.Text.Position(startX, currentY);

                // Add the fragment to the index page
                indexPage.Paragraphs.Add(tf);

                // Move down for the next entry
                currentY -= lineHeight;
            }

            // Save the combined PDF document
            finalPdf.Save(outputPdfPath);
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up the temporary folder if it still exists
            try
            {
                if (Directory.Exists(tempFolder))
                {
                    Directory.Delete(tempFolder, true);
                }
            }
            catch
            {
                // Suppress any cleanup errors
            }
        }
    }
}