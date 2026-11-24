using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // -----------------------------------------------------------------
                // Create an index page that will be placed at the beginning of the document
                // -----------------------------------------------------------------
                Page indexPage = new Page();
                indexPage.Name = "Index";
                diagram.Pages.Add(indexPage);
                // Move the newly added index page to the first position (index 0)
                indexPage.MoveTo(0);

                // Layout parameters for index entries (in inches)
                double startX = 1.0;          // left margin
                double startY = 1.0;          // top margin
                double entryHeight = 0.5;     // height of each entry rectangle
                double entryWidth = 6.0;      // width of each entry rectangle
                double verticalSpacing = 0.2; // space between entries

                int pageNumber = 1;
                foreach (Page srcPage in diagram.Pages)
                {
                    // Skip the index page itself
                    if (srcPage == indexPage)
                        continue;

                    // Calculate Y position for this entry
                    double posY = startY + (entryHeight + verticalSpacing) * (pageNumber - 1);

                    // Draw a rectangle shape that will hold the page title (and could hold a thumbnail)
                    long shapeId = indexPage.DrawRectangle(startX, posY, entryWidth, entryHeight);
                    Shape entryShape = indexPage.Shapes.GetShape(shapeId);

                    // Clear any existing text (should be empty) and add new text run
                    entryShape.Text.Value.Clear();
                    string pageName = !string.IsNullOrEmpty(srcPage.Name) ? srcPage.Name : $"Page{pageNumber}";
                    entryShape.Text.Value.Add(new Txt($"Page {pageNumber}: {pageName}"));

                    // TODO: Insert a thumbnail image of srcPage into the entryShape or adjacent shape.
                    // This would involve exporting srcPage to an image and adding it as a picture shape.

                    pageNumber++;
                }

                // -----------------------------------------------------------------
                // Prepare PDF save options
                // -----------------------------------------------------------------
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Explicitly set the format tracker as required
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;
                // Export hidden pages if any (set to false for typical printable output)
                pdfOptions.ExportHiddenPage = false;
                // Set high resolution for raster content inside the PDF
                pdfOptions.HorizontalResolution = 300;
                pdfOptions.VerticalResolution = 300;
                // Use Flate compression to reduce file size
                pdfOptions.TextCompression = PdfTextCompression.Flate;
                // Use a fallback font in case the diagram references missing fonts
                pdfOptions.DefaultFont = "Arial";

                // -----------------------------------------------------------------
                // Save the diagram (including the index page) as a PDF
                // -----------------------------------------------------------------
                diagram.Save(outputPath, pdfOptions);
                Console.WriteLine($"PDF successfully created at: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }
}