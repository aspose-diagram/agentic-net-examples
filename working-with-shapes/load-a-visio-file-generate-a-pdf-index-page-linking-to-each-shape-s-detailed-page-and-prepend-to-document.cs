using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input Visio file and output PDF file
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: VisioIndexPdfGenerator <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard for input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // ------------------------------------------------------------
            // 1. Create an index page and insert it at the beginning
            // ------------------------------------------------------------
            Page indexPage = new Page();                     // new blank page
            diagram.Pages.Add(indexPage);                     // add to collection
            indexPage.MoveTo(0);                              // move to first position
            indexPage.Name = "Index";                         // optional name
            indexPage.NameU = "Index";

            // ------------------------------------------------------------
            // 2. Build the index content (list of shapes with hyperlinks)
            // ------------------------------------------------------------
            double startX = 1.0;      // inches from left margin
            double startY = 1.0;      // inches from top margin
            double lineHeight = 0.5;  // vertical spacing between entries
            double boxWidth = 5.0;    // width of the text box
            double boxHeight = 0.4;   // height of the text box

            double currentY = startY;

            // Iterate over all pages (skip the index page itself)
            foreach (Page page in diagram.Pages)
            {
                if (page == indexPage) continue; // do not list the index page

                // Iterate over each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Build display text: "PageName - ShapeNameU"
                    string displayText = $"{page.NameU} - {shape.NameU}";

                    // Add a rectangle shape on the index page to hold the entry
                    long idxShapeId = indexPage.AddShape(startX, currentY, boxWidth, boxHeight, "Rectangle", false);
                    Shape idxShape = indexPage.Shapes.GetShape(idxShapeId);

                    // Clear any default text and add our display text
                    idxShape.Text.Value.Clear();
                    idxShape.Text.Value.Add(new Txt(displayText));

                    // Create a hyperlink that navigates to the target page
                    Hyperlink link = new Hyperlink();
                    // SubAddress points to the page name; Visio treats this as an internal link
                    link.SubAddress.Value = page.NameU;
                    idxShape.Hyperlinks.Add(link);

                    // Move to the next line position
                    currentY += lineHeight;
                }
            }

            // ------------------------------------------------------------
            // 3. Save the updated diagram as a PDF
            // ------------------------------------------------------------
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial"; // fallback font
            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}