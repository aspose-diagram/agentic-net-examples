using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output PDF file path
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <program> <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPdfPath = args[1];

        // Load the Visio diagram
        using (Diagram diagram = new Diagram(inputPath))
        {
            // Create a new page that will hold the index of thumbnails
            Page indexPage = new Page();
            indexPage.Name = "Index";
            diagram.Pages.Add(indexPage);
            // Move the index page to the first position so it appears first in the PDF
            indexPage.MoveTo(0);

            // Thumbnail layout settings (all dimensions are in inches)
            const double thumbWidth = 2.0;
            const double thumbHeight = 2.0;
            const double marginX = 0.5;
            const double marginY = 0.5;
            const double hSpacing = 0.2;
            const double vSpacing = 0.2;
            const int columns = 3;

            // Iterate over the original pages (skip the newly added index page at position 0)
            for (int i = 1; i < diagram.Pages.Count; i++)
            {
                // Calculate grid position for the current thumbnail
                int col = (i - 1) % columns;
                int row = (i - 1) / columns;
                double pinX = marginX + col * (thumbWidth + hSpacing);
                double pinY = marginY + row * (thumbHeight + vSpacing);

                // Export the current page to a PNG image stored in a memory stream
                using (MemoryStream imgStream = new MemoryStream())
                {
                    ImageSaveOptions imgOpts = new ImageSaveOptions(SaveFileFormat.Png);
                    imgOpts.PageIndex = i;               // Export page i (zero‑based index)
                    imgOpts.ExportHiddenPage = false;    // Do not include hidden pages
                    diagram.Save(imgStream, imgOpts);
                    imgStream.Position = 0;

                    // Insert the image as a foreign shape on the index page
                    long shapeId = indexPage.AddShape(pinX, pinY, thumbWidth, thumbHeight, imgStream);
                    // Optionally retrieve the shape if further customization is needed
                    // Shape thumbShape = indexPage.Shapes.GetShape(shapeId);
                }
            }

            // Prepare PDF save options
            PdfSaveOptions pdfOpts = new PdfSaveOptions();
            pdfOpts.DefaultFont = "Arial"; // Fallback font for missing characters

            // Save the diagram (including the index page) as a PDF
            diagram.Save(outputPdfPath, pdfOpts);
        }

        Console.WriteLine("PDF with index page generated successfully.");
    }
}
