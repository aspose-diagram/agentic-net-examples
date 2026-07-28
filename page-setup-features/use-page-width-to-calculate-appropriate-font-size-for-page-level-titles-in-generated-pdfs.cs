using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new empty diagram
        using (Diagram diagram = new Diagram())
        {
            // Add a new page to the diagram
            Page page = new Page();
            diagram.Pages.Add(page);

            // Set page dimensions (A4 size in inches)
            page.PageSheet.PageProps.PageWidth.Value = 8.27;   // width
            page.PageSheet.PageProps.PageHeight.Value = 11.69; // height

            // Calculate title font size as a proportion of the page width (e.g., 5%)
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double titleFontSizeInInches = pageWidth * 0.05; // 5% of page width

            // Title text and formatting
            string titleText = "Document Title";
            string fontName = "Arial";
            string fontColor = "#000000";

            // Position the title at the top center of the page
            double pinX = pageWidth / 2.0;                                   // center horizontally
            double pinY = page.PageSheet.PageProps.PageHeight.Value - 0.5;   // half‑inch from top edge
            double titleBoxWidth = pageWidth * 0.8;                          // 80% of page width
            double titleBoxHeight = titleFontSizeInInches * 2;               // enough height for the text

            // Add the title shape with the calculated font size (size is in inches)
            Shape titleShape = page.AddText(pinX, pinY, titleBoxWidth, titleBoxHeight,
                                            titleText, fontName, fontColor, titleFontSizeInInches);

            // Configure PDF save options and set a default font for fallback
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = fontName;
            pdfOptions.SaveFormat = SaveFileFormat.Pdf; // explicit format

            // Save the diagram as a PDF file
            diagram.Save("output.pdf", pdfOptions);
        }
    }
}
