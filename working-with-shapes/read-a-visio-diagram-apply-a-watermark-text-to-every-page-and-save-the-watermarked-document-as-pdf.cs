using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output PDF file path
                string outputPath = "output.pdf";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page and add a watermark text
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Center position for the watermark
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Add watermark text covering the full page
                    // Font size is specified in inches (e.g., 0.5 inches ≈ 36 points)
                    page.AddText(pinX, pinY, pageWidth, pageHeight,
                                 "CONFIDENTIAL",          // watermark text
                                 "Arial",                // font name
                                 "#C0C0C0",              // light gray color in hex
                                 0.5);                   // font size in inches
                }

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";

                // Save the diagram as a PDF with the watermarks applied
                diagram.Save(outputPath, pdfOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }