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
            Console.Error.WriteLine("Usage: DiagramReport <inputVisioPath> <outputPdfPath>");
            return;
        }

        string visioPath = args[0];
        // Guard: ensure the Visio file exists.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        string pdfPath = args[1];
        // Guard: ensure the output directory exists (create if necessary).
        string pdfDir = Path.GetDirectoryName(pdfPath);
        if (!string.IsNullOrEmpty(pdfDir) && !Directory.Exists(pdfDir))
        {
            try { Directory.CreateDirectory(pdfDir); }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory: {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(visioPath);

            // Create a new PDF document (fully qualified Aspose.Pdf namespace to avoid ambiguity).
            Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document();

            // Iterate over all pages in the diagram.
            int pageIndex = 0; // zero‑based index required by ImageSaveOptions.
            foreach (Page page in diagram.Pages)
            {
                // Add a new page to the PDF for this Visio page.
                Aspose.Pdf.Page pdfPage = pdfDoc.Pages.Add();

                // Determine if the Visio page is hidden (UIVisibility.Value is UIVisibilityValue).
                bool isHidden = page.PageSheet.PageProps.UIVisibility.Value == UIVisibilityValue.Hidden;

                // Build a metadata string for the current Visio page.
                string meta = $"Page Index: {pageIndex}\n" +
                              $"Page ID: {page.ID}\n" +
                              $"Name: {page.Name}\n" +
                              $"Universal Name: {page.NameU}\n" +
                              $"Width (in): {page.PageSheet.PageProps.PageWidth.Value}\n" +
                              $"Height (in): {page.PageSheet.PageProps.PageHeight.Value}\n" +
                              $"Hidden: {isHidden}";

                // Add the metadata as a text fragment.
                Aspose.Pdf.Text.TextFragment tf = new Aspose.Pdf.Text.TextFragment(meta);
                tf.TextState.FontSize = 12; // readable font size.
                tf.TextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("Arial");
                tf.Margin = new Aspose.Pdf.MarginInfo { Top = 20, Left = 20 };
                pdfPage.Paragraphs.Add(tf);

                // If the page is hidden, generate a thumbnail image.
                if (isHidden)
                {
                    // Configure image export options for a single page.
                    ImageSaveOptions imgOpts = new ImageSaveOptions(SaveFileFormat.Png);
                    imgOpts.PageIndex = pageIndex;          // render the current page.
                    imgOpts.ExportHiddenPage = true;        // allow hidden page rendering.
                    imgOpts.Resolution = 150;               // reasonable DPI for a thumbnail.

                    // Export the page to a memory stream.
                    using (MemoryStream imgStream = new MemoryStream())
                    {
                        diagram.Save(imgStream, imgOpts);
                        imgStream.Position = 0; // reset stream for reading.

                        // Create an Aspose.Pdf image from the stream.
                        Aspose.Pdf.Image pdfImg = new Aspose.Pdf.Image();
                        pdfImg.ImageStream = imgStream;

                        // Scale the image to fit within the PDF page width (optional).
                        pdfImg.FixWidth = pdfPage.PageInfo.Width - 40; // leave margins.

                        // Add a small vertical gap before the image.
                        pdfPage.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("\nThumbnail:"));
                        // Insert the image into the PDF page.
                        pdfPage.Paragraphs.Add(pdfImg);
                    }
                }

                // Increment the page index for the next iteration.
                pageIndex++;
            }

            // Save the assembled PDF report.
            pdfDoc.Save(pdfPath);
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}