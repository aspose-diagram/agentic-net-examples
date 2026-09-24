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

            // Paths for input Visio file and output PDF
            string inputPath = "input.vsdx";
            string outputPdfPath = "output.pdf";

            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Create a new page that will serve as the index
            Page indexPage = new Page();
            diagram.Pages.Add(indexPage);
            // Move the index page to the first position
            indexPage.MoveTo(0);

            // Layout parameters for index entries
            double startX = 1.0;          // horizontal position
            double startY = 1.0;          // initial vertical position
            double entryHeight = 0.3;     // height of each entry shape
            double entryWidth = 5.0;      // width of each entry shape
            double verticalSpacing = 0.5; // space between entries
            int entryCounter = 0;

            // Iterate through all pages (skip the newly added index page)
            foreach (Page page in diagram.Pages)
            {
                if (page == indexPage)
                    continue;

                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Build the display text for the index entry
                    string entryText = $"{page.NameU} - {shape.NameU}";

                    // Calculate position for this entry
                    double pinX = startX;
                    double pinY = startY + entryCounter * verticalSpacing;

                    // Add a text shape to the index page
                    Shape entryShape = indexPage.AddText(pinX, pinY, entryWidth, entryHeight, entryText);

                    // Create a hyperlink that points to the page containing the shape
                    Hyperlink link = new Hyperlink();
                    // SubAddress refers to the target page name within the Visio document
                    link.SubAddress.Value = page.NameU;
                    // Optional tooltip
                    link.Description.Value = $"Go to {page.NameU}";
                    entryShape.Hyperlinks.Add(link);

                    entryCounter++;
                }
            }

            // Configure PDF save options (set a default font to avoid missing font warnings)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";

            // Save the updated diagram as a PDF (index page will be the first page)
            diagram.Save(outputPdfPath, pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
