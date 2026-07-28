using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        const string visioPath = "input.vsdx";
        // Guard: ensure the Visio file exists
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        // Path for the generated PDF report
        const string pdfReportPath = "Report.pdf";

        try
        {
            // Load the Visio diagram
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Create an Aspose.Pdf document (fully qualified to avoid namespace conflict)
                var pdfDoc = new Aspose.Pdf.Document();

                // Iterate through all pages in the diagram
                int pageIndex = 0;
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    // ----- Gather page metadata -----
                    string metadata = $"Page Index: {pageIndex}{Environment.NewLine}" +
                                      $"Page ID: {page.ID}{Environment.NewLine}" +
                                      $"Name: {page.Name}{Environment.NewLine}" +
                                      $"Universal Name: {page.NameU}{Environment.NewLine}" +
                                      $"Is Background: {page.Background == Aspose.Diagram.BOOL.True}{Environment.NewLine}" +
                                      $"Width (in): {page.PageSheet.PageProps.PageWidth.Value}{Environment.NewLine}" +
                                      $"Height (in): {page.PageSheet.PageProps.PageHeight.Value}{Environment.NewLine}";

                    // ----- Add a new PDF page for this diagram page -----
                    Aspose.Pdf.Page pdfPage = pdfDoc.Pages.Add();

                    // Add metadata text
                    var textFragment = new Aspose.Pdf.Text.TextFragment(metadata);
                    pdfPage.Paragraphs.Add(textFragment);

                    // ----- Export the current diagram page as a PNG thumbnail -----
                    var imgOptions = new ImageSaveOptions(SaveFileFormat.Png)
                    {
                        ExportHiddenPage = true,   // Include hidden pages in the export
                        PageIndex = pageIndex,     // Export the current page only
                        PageCount = 1              // Ensure only one page is rendered
                    };

                    using (var imgStream = new MemoryStream())
                    {
                        // Save the single page as an image to the memory stream
                        diagram.Save(imgStream, imgOptions);
                        imgStream.Position = 0; // Reset stream position for reading

                        // Create an Aspose.Pdf image from the stream (use parameterless ctor then set stream)
                        var pdfImage = new Aspose.Pdf.Image();
                        pdfImage.ImageStream = imgStream; // Assign the image data
                        pdfImage.FixWidth = 200;          // Optionally set image width (points)

                        // Add the image below the metadata text
                        pdfPage.Paragraphs.Add(pdfImage);
                    }

                    pageIndex++;
                }

                // Save the assembled PDF report
                pdfDoc.Save(pdfReportPath);
            }

            Console.WriteLine("PDF report generated successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}