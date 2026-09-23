using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class Program
{
    public static void Main()
    {
        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Access the first (default) page
        Page page = diagram.Pages[0];

        // Retrieve page dimensions (in inches)
        double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
        double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

        // Calculate font size based on page width.
        // Example: 10 points per inch of page width.
        double fontSizePoints = pageWidth * 10.0;
        // Convert points to inches (Aspose.Diagram expects size in inches)
        double fontSizeInches = fontSizePoints / 72.0;

        // Define title shape dimensions
        double titleWidth = pageWidth * 0.8;      // 80% of page width
        double titleHeight = 0.5;                // half an inch tall
        double titlePinX = pageWidth / 2.0;      // centered horizontally
        double titlePinY = pageHeight - titleHeight / 2.0 - 0.2; // near top with margin

        // Add a rectangle shape to serve as the title placeholder
        long titleShapeId = page.DrawRectangle(titlePinX, titlePinY, titleWidth, titleHeight);
        Shape titleShape = page.Shapes.GetShape(titleShapeId);

        // Clear any existing text and add the title text
        titleShape.Text.Value.Clear();
        titleShape.Text.Value.Add(new Txt("Page Title"));

        // Create character formatting for the title text
        Aspose.Diagram.Char titleChar = new Aspose.Diagram.Char();
        titleChar.IX = 0;                         // first character run
        titleChar.FontName.Value = "Arial";       // fallback font
        titleChar.Size.Value = fontSizeInches;    // calculated size in inches
        titleChar.Style.Value |= StyleValue.Bold; // make the title bold
        titleShape.Chars.Add(titleChar);

        // Configure PDF save options
        PdfSaveOptions pdfOptions = new PdfSaveOptions();
        pdfOptions.DefaultFont = "Arial";
        pdfOptions.SaveFormat = SaveFileFormat.Pdf;

        // Save the diagram as a PDF
        diagram.Save("output.pdf", pdfOptions);
    }
}
