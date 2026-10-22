using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string visioPath = args[0];
        // Guard: check if the Visio file exists
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Visio file not found: {visioPath}");
            return;
        }

        // Optional second argument: output directory
        string outputDir = args.Length > 1 ? args[1] : Path.GetDirectoryName(visioPath);
        // Guard: check if the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Iterate through each page in the diagram
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                // Retrieve the current page
                Page page = diagram.Pages[pageIndex];

                // Define the output PDF file name for this page
                string pdfPath = Path.Combine(outputDir, $"Page_{pageIndex + 1}.pdf");

                // Configure PDF save options to export only the current page
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.PageIndex = pageIndex;   // zero‑based index of the page to start saving
                pdfOptions.PageCount = 1;           // export a single page
                pdfOptions.ExportHiddenPage = false;

                // Save the selected page to a PDF file
                diagram.Save(pdfPath, pdfOptions);

                // Load the generated PDF to add bookmarks for each shape
                Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(pdfPath);

                // The PDF contains only one page; retrieve it (Aspose.Pdf pages are 1‑based)
                Aspose.Pdf.Page pdfPage = pdfDoc.Pages[1];

                // Add a bookmark (outline item) for each shape on the Visio page
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Use the shape's NameU if available; otherwise fall back to its ID
                    string title = !string.IsNullOrEmpty(shape.NameU) ? shape.NameU : $"Shape {shape.ID}";

                    // Create a new bookmark linked to the single PDF page
                    Aspose.Pdf.OutlineItemCollection bookmark = new Aspose.Pdf.OutlineItemCollection(pdfDoc.Outlines)
                    {
                        Title = title,
                        Action = new Aspose.Pdf.Annotations.GoToAction(pdfPage)
                    };

                    // Add the bookmark to the PDF's outline collection
                    pdfDoc.Outlines.Add(bookmark);
                }

                // Save the PDF with the newly added bookmarks
                pdfDoc.Save(pdfPath);
                Console.WriteLine($"Page {pageIndex + 1} saved to {pdfPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"Error processing Visio file: {ex.Message}");
        }
    }
}