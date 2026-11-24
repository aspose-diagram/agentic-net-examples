using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> [outputPdfPath]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length > 1 ? args[1] : Path.ChangeExtension(inputPath, ".pdf");

        // Guard: ensure the input Visio file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page and assign a light gray background via a background page
            foreach (Page page in diagram.Pages)
            {
                // Skip pages that are already background pages
                if (page.Background == BOOL.True)
                    continue;

                // Create a new background page
                Page backgroundPage = new Page();
                backgroundPage.Name = page.Name + "_Background";
                backgroundPage.Background = BOOL.True;

                // Add the background page to the diagram
                diagram.Pages.Add(backgroundPage);

                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Add a rectangle shape that covers the entire page
                // Master name "Rectangle" is a standard Visio shape
                long shapeId = backgroundPage.AddShape(0, 0, "Rectangle", false);
                Shape backgroundShape = backgroundPage.Shapes.GetShape(shapeId);

                // Position the rectangle at the center of the page
                backgroundShape.XForm.PinX.Value = pageWidth / 2.0;
                backgroundShape.XForm.PinY.Value = pageHeight / 2.0;
                backgroundShape.XForm.Width.Value = pageWidth;
                backgroundShape.XForm.Height.Value = pageHeight;

                // Set solid fill pattern
                backgroundShape.Fill.FillPattern.Value = 1; // Solid fill
                // Light gray color (hex #D3D3D3)
                backgroundShape.Fill.FillForegnd.Value = "#D3D3D3";

                // Link the foreground page to the newly created background page
                page.BackPage = backgroundPage;
            }

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.SaveFormat = SaveFileFormat.Pdf; // Explicitly set format
            pdfOptions.ExportHiddenPage = false;        // Do not export hidden pages
            pdfOptions.TextCompression = PdfTextCompression.Flate; // Optional compression

            // Save the diagram as PDF
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Successfully exported PDF to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}