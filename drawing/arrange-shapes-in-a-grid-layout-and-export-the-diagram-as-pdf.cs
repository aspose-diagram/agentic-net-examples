using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define the output PDF file path
        string outputPdfPath = "GridDiagram.pdf";

        // Guard: ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPdfPath));
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Create a new empty diagram
            Diagram diagram = new Diagram();

            // Ensure there is at least one page to work with
            if (diagram.Pages.Count == 0)
            {
                diagram.Pages.Add(new Page());
            }

            // Use the first page for shape placement
            Page page = diagram.Pages[0];

            // Grid layout parameters
            int rows = 3;
            int columns = 3;
            double startX = 2.0;      // Starting X coordinate (in inches)
            double startY = 2.0;      // Starting Y coordinate (in inches)
            double spacingX = 2.5;    // Horizontal spacing between shapes (in inches)
            double spacingY = 2.5;    // Vertical spacing between shapes (in inches)
            double shapeWidth = 1.5;  // Width of each rectangle (in inches)
            double shapeHeight = 1.0; // Height of each rectangle (in inches)

            // Add rectangle shapes in a grid pattern
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    double pinX = startX + col * spacingX;
                    double pinY = startY + row * spacingY;

                    // Draw a rectangle; returns the shape ID
                    long shapeId = page.DrawRectangle(pinX, pinY, shapeWidth, shapeHeight);

                    // Retrieve the shape object to set its text
                    Shape shape = page.Shapes.GetShape(shapeId);
                    if (shape != null)
                    {
                        // Clear any existing text and add new text run
                        shape.Text.Value.Clear();
                        shape.Text.Value.Add(new Txt($"R{row + 1}C{col + 1}"));
                    }
                }
            }

            // Configure PDF save options with a page‑saving callback
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;
            pdfOptions.PageSavingCallback = new PdfPageCallback();

            // Save the diagram as a PDF file
            diagram.Save(outputPdfPath, pdfOptions);

            Console.WriteLine($"Diagram successfully saved to PDF: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error during diagram processing: {ex.Message}");
        }
    }
}

// Custom callback to log page‑saving events
class PdfPageCallback : IPageSavingCallback
{
    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}");
    }

    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"Finished saving page {args.PageIndex + 1} of {args.PageCount}");
        // Example: stop after the first page (not required here)
        // args.HasMorePages = false;
    }
}