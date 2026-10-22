using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // -----------------------------------------------------------------
            // Create a background page with a white rectangle covering the page
            // -----------------------------------------------------------------
            // Assume at least one page exists to obtain dimensions
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Use the first page to get width and height (in inches)
            Page referencePage = diagram.Pages[0];
            double pageWidth = referencePage.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = referencePage.PageSheet.PageProps.PageHeight.Value;

            // Add a new page that will serve as the background
            Page backgroundPage = new Page();
            diagram.Pages.Add(backgroundPage);
            backgroundPage.Name = "BackgroundWhite";
            backgroundPage.Background = BOOL.True; // Mark as background page

            // Add a rectangle shape that spans the whole page
            // Master name "Rectangle" is a built‑in Visio shape
            long rectShapeId = backgroundPage.AddShape(0, 0, "Rectangle", false);
            Shape rectShape = backgroundPage.Shapes.GetShape(rectShapeId);

            // Position the rectangle at the center of the page
            rectShape.XForm.PinX.Value = pageWidth / 2.0;
            rectShape.XForm.PinY.Value = pageHeight / 2.0;
            rectShape.XForm.Width.Value = pageWidth;
            rectShape.XForm.Height.Value = pageHeight;

            // Set solid white fill
            rectShape.Fill.FillPattern.Value = 1;               // Solid fill
            rectShape.Fill.FillForegnd.Value = "#FFFFFF";       // White color

            // Remove border line
            rectShape.Line.LinePattern.Value = 0;               // No line

            // ---------------------------------------------------------------
            // Assign the background page to each foreground page in the diagram
            // ---------------------------------------------------------------
            foreach (Page page in diagram.Pages)
            {
                // Skip the background page itself
                if (page.Background == BOOL.True)
                    continue;

                // Link the foreground page to the created background page
                page.BackPage = backgroundPage;
            }

            // ---------------------------------------------------------------
            // Export each page as a separate PDF file
            // ---------------------------------------------------------------
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                Page page = diagram.Pages[i];

                // Skip background page – we only want foreground pages in PDFs
                if (page.Background == BOOL.True)
                    continue;

                string outputPdf = $"Page_{i + 1}.pdf";

                // Configure PDF save options to export only the current page
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.PageIndex = i;          // Zero‑based page index
                pdfOptions.PageCount = 1;          // Export a single page
                pdfOptions.SaveFormat = SaveFileFormat.Pdf; // Explicit format

                // Save the diagram (only the specified page) to PDF
                diagram.Save(outputPdf, pdfOptions);

                Console.WriteLine($"Exported page {i + 1} to {outputPdf}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}