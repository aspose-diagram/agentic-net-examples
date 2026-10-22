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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare a list of pages for indexed access.
                List<Page> pages = new List<Page>();
                foreach (Page p in diagram.Pages)
                {
                    pages.Add(p);
                }

                // Create a new PDF document (Aspose.Pdf) using fully qualified names.
                Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document();

                // Iterate over each page to collect metadata and, if hidden, add a thumbnail.
                for (int i = 0; i < pages.Count; i++)
                {
                    Page page = pages[i];

                    // Determine visibility using UIVisibilityValue.
                    string visibility = page.PageSheet.PageProps.UIVisibility.Value == UIVisibilityValue.Visible
                        ? "Visible"
                        : "Hidden";

                    // Add a new page to the PDF report.
                    Aspose.Pdf.Page pdfPage = pdfDocument.Pages.Add();

                    // Create a text fragment with page metadata.
                    string metaText = $"Page {i + 1}\nName: {page.Name}\nID: {page.ID}\nVisibility: {visibility}";
                    Aspose.Pdf.Text.TextFragment textFragment = new Aspose.Pdf.Text.TextFragment(metaText);
                    textFragment.Margin = new Aspose.Pdf.MarginInfo { Top = 10, Bottom = 10, Left = 10, Right = 10 };
                    pdfPage.Paragraphs.Add(textFragment);

                    // If the page is hidden, generate a PNG thumbnail and embed it.
                    if (visibility == "Hidden")
                    {
                        // Configure image save options for a single-page PNG export.
                        ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                        imgOptions.PageIndex = i;   // Export only this page.
                        imgOptions.PageCount = 1;   // Single page per image.
                        imgOptions.ExportHiddenPage = true; // Ensure hidden pages are rendered.

                        // Render the page to a memory stream.
                        using (MemoryStream imgStream = new MemoryStream())
                        {
                            diagram.Save(imgStream, imgOptions);
                            imgStream.Position = 0; // Reset stream for reading.

                            // Create an image object for the PDF page.
                            Aspose.Pdf.Image pdfImage = new Aspose.Pdf.Image();
                            pdfImage.ImageStream = imgStream;
                            pdfImage.FixWidth = 300; // Scale thumbnail width; height adjusts proportionally.
                            pdfImage.Margin = new Aspose.Pdf.MarginInfo { Top = 10, Bottom = 10, Left = 10, Right = 10 };
                            pdfPage.Paragraphs.Add(pdfImage);
                        }
                    }
                }

                // Save the assembled PDF report.
                pdfDocument.Save(outputPath);
                Console.WriteLine($"PDF report generated successfully at: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}