using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (adjust as needed)
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Desired border thickness (in inches)
            double borderThickness = 0.03; // approx 2.16 points

            // Iterate pages using an index because Page has no Index property
            for (int pageIdx = 0; pageIdx < diagram.Pages.Count; pageIdx++)
            {
                Page page = diagram.Pages[pageIdx];

                // Apply border thickness to all non‑deleted shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Set line weight (border thickness)
                    shape.Line.LineWeight.Value = borderThickness;
                }

                // Prepare PDF save options for the current page
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    // Export only the current page (zero‑based index)
                    PageIndex = pageIdx,
                    ExportHiddenPage = false,
                    // Fallback font in case a required font is missing
                    DefaultFont = "Arial"
                };

                // Construct output PDF file name using the page name (or a fallback)
                string safePageName = string.IsNullOrWhiteSpace(page.Name) ? $"Page_{pageIdx}" : page.Name;
                string outputPdfPath = Path.Combine("Output", $"{safePageName}.pdf");

                // Ensure the output directory exists
                Directory.CreateDirectory(Path.GetDirectoryName(outputPdfPath) ?? "Output");

                // Save the current page as a PDF
                diagram.Save(outputPdfPath, pdfOptions);
            }

            Console.WriteLine("Processing completed. PDFs are saved in the 'Output' folder.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}