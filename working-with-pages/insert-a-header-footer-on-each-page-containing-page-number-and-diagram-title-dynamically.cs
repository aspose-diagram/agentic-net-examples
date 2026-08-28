using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Insert dynamic header and footer.
            // Visio field codes are used inside the strings:
            // &[Page]   – current page number
            // &[Pages]  – total number of pages
            // &[DocTitle] – document title (from DocumentProps.Title)
            diagram.HeaderFooter.HeaderCenter = "Page &[Page] of &[Pages]";
            diagram.HeaderFooter.FooterCenter = "Title: &[DocTitle]";

            // Optional: set font color (e.g., dark gray) and margins if needed
            // diagram.HeaderFooter.HeaderFooterColor = 0x404040; // RGB hex
            // diagram.HeaderFooter.HeaderMargin = 0.2; // inches
            // diagram.HeaderFooter.FooterMargin = 0.2; // inches

            // Save the diagram as PDF, splitting each Visio page into a separate PDF page
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                SplitMultiPages = true
            };
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
