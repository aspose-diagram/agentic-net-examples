using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Create a new diagram instance
        using Diagram diagram = new Diagram();

        // Ensure there is at least one page to work with
        Page page;
        if (diagram.Pages.Count == 0)
        {
            page = new Page();
            diagram.Pages.Add(page);
        }
        else
        {
            page = diagram.Pages[0];
        }

        // Retrieve current page dimensions (in inches)
        double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
        double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

        // If the diagram is brand‑new the dimensions may be zero – set A4 defaults
        if (pageWidth == 0 || pageHeight == 0)
        {
            pageWidth = 8.27;   // A4 width in inches
            pageHeight = 11.69; // A4 height in inches
            page.PageSheet.PageProps.PageWidth.Value = pageWidth;
            page.PageSheet.PageProps.PageHeight.Value = pageHeight;
        }

        // Calculate a font size that scales with the page width.
        // Example: use 10 % of the page width expressed in points.
        double fontSizePoints = pageWidth * 10.0;
        double fontSizeInches = fontSizePoints / 72.0; // convert points → inches

        // Title text to be placed on the page
        string title = "Sample Document Title";

        // Position the title near the top centre of the page
        double marginTop = 0.5; // inches from the top edge
        double pinX = pageWidth / 2.0;
        double pinY = pageHeight - marginTop;

        // Define a text box that spans the page width
        double textBoxWidth = pageWidth;
        double textBoxHeight = fontSizeInches * 2.0; // enough height for the text

        // Add the title shape with the calculated font size
        Shape titleShape = page.AddText(pinX, pinY, textBoxWidth, textBoxHeight,
                                        title, "Arial", "#000000", fontSizeInches);

        // Center the text horizontally within the box
        titleShape.TextXForm.TxtLocPinX.Value = 0.5 * textBoxWidth;
        titleShape.TextXForm.TxtLocPinY.Value = 0.0; // top alignment

        // Prepare PDF save options and enforce a default font
        PdfSaveOptions pdfOptions = new PdfSaveOptions();
        pdfOptions.DefaultFont = "Arial";
        pdfOptions.SaveFormat = SaveFileFormat.Pdf; // explicit format

        // Save the diagram as a PDF
        diagram.Save("output.pdf", pdfOptions);
    }
}
