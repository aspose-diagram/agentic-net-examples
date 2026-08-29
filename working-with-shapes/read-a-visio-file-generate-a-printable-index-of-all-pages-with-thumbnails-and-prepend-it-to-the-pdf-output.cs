using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file and output PDF paths
            string inputPath = "input.vsdx";
            string outputPath = "output.pdf";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Create a new page that will serve as the index
            Page indexPage = new Page();
            indexPage.Name = "Index";
            indexPage.NameU = "Index";

            // Add the index page to the diagram
            diagram.Pages.Add(indexPage);
            // Move it to the first position so it appears before all other pages
            indexPage.MoveTo(0);

            // Layout parameters for thumbnails
            double thumbWidth = 2.0;   // inches
            double thumbHeight = 2.0;  // inches
            double marginX = 0.5;      // inches from left
            double marginY = 0.5;      // inches from top
            double spacingX = 0.3;     // horizontal spacing between thumbnails
            double spacingY = 0.3;     // vertical spacing between thumbnails

            // Determine how many columns we want per row
            int columns = 3;
            int currentColumn = 0;
            int currentRow = 0;

            // Iterate over all pages in the original diagram (excluding the index page we just added)
            foreach (Page page in diagram.Pages)
            {
                // Skip the index page itself
                if (page == indexPage)
                    continue;

                // Export the current page to a PNG thumbnail in memory
                using (MemoryStream thumbStream = new MemoryStream())
                {
                    ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                    imgOptions.PageIndex = page.ID; // Render this specific page
                    imgOptions.PageSize = new PageSize((float)thumbWidth, (float)thumbHeight);
                    diagram.Save(thumbStream, imgOptions);
                    thumbStream.Position = 0;

                    // Calculate position for the thumbnail on the index page
                    double pinX = marginX + currentColumn * (thumbWidth + spacingX) + thumbWidth / 2.0;
                    double pinY = marginY + currentRow * (thumbHeight + spacingY) + thumbHeight / 2.0;

                    // Insert the thumbnail image as a foreign shape on the index page
                    long shapeId = indexPage.AddShape(pinX, pinY, thumbWidth, thumbHeight, thumbStream);
                    // (Optional) retrieve the shape if further formatting is needed
                    // Shape thumbShape = indexPage.Shapes.GetShape(shapeId);
                }

                // Update column/row counters
                currentColumn++;
                if (currentColumn >= columns)
                {
                    currentColumn = 0;
                    currentRow++;
                }
            }

            // Prepare PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial"; // Fallback font for missing characters
            pdfOptions.ExportHiddenPage = false; // Do not export hidden pages

            // Save the diagram (including the index page) as a PDF
            diagram.Save(outputPath, pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
