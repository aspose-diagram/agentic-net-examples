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
            // Output PDF file path (will contain the index page as the first page)
            string outputPdfPath = "output.pdf";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Store thumbnails for each page in memory
            List<MemoryStream> thumbnails = new List<MemoryStream>();
            int pageCount = diagram.Pages.Count;

            for (int i = 0; i < pageCount; i++)
            {
                // Export the i‑th page to a PNG image stored in a MemoryStream
                ImageSaveOptions imgOpts = new ImageSaveOptions(SaveFileFormat.Png);
                imgOpts.PageIndex = i;               // zero‑based page index
                imgOpts.ExportHiddenPage = false;    // ignore hidden pages

                MemoryStream ms = new MemoryStream();
                diagram.Save(ms, imgOpts);
                ms.Position = 0;                     // reset stream for reading
                thumbnails.Add(ms);
            }

            // Create a new page that will serve as the index
            Page indexPage = new Page();
            diagram.Pages.Add(indexPage);
            // Move the index page to the first position (page index 0)
            indexPage.MoveTo(0);

            // Layout parameters for thumbnails on the index page
            double thumbWidth = 2.0;   // inches
            double thumbHeight = 2.0;  // inches
            double margin = 0.5;       // inches between thumbnails
            int columns = 3;           // thumbnails per row

            int col = 0;
            int row = 0;

            // Add each thumbnail image as a shape on the index page
            foreach (MemoryStream thumbStream in thumbnails)
            {
                double pinX = margin + col * (thumbWidth + margin);
                double pinY = margin + row * (thumbHeight + margin);

                // AddShape returns the shape ID (long). The shape is automatically created from the image stream.
                indexPage.AddShape(pinX, pinY, thumbWidth, thumbHeight, thumbStream);

                // Advance grid position
                col++;
                if (col >= columns)
                {
                    col = 0;
                    row++;
                }
            }

            // Save the final diagram as a PDF; the index page is now the first page.
            PdfSaveOptions pdfOpts = new PdfSaveOptions();
            pdfOpts.DefaultFont = "Arial"; // fallback font for missing characters
            diagram.Save(outputPdfPath, pdfOpts);

            // Clean up memory streams
            foreach (var ms in thumbnails)
            {
                ms.Dispose();
            }

            // No explicit Dispose needed for Diagram (it implements IDisposable but will be collected)
            Console.WriteLine("PDF with index page generated successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
