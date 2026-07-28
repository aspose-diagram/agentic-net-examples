using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path and output PDF path
        string inputPath = "input.vsdx";
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }
        string outputPath = "output.pdf";

        // Load the diagram (Diagram does not implement IDisposable, so no using block)
        Diagram diagram = new Diagram(inputPath);
        try
        {
            // Ensure there is at least one page to work with
            if (diagram.Pages.Count == 0)
            {
                Console.WriteLine("The diagram contains no pages.");
                return;
            }

            // Get the first (foreground) page
            Page foregroundPage = diagram.Pages[0];

            // Create a new background page
            Page backgroundPage = new Page();
            backgroundPage.Background = BOOL.True; // Mark as background page

            // Match the size of the foreground page
            double pageWidth = foregroundPage.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = foregroundPage.PageSheet.PageProps.PageHeight.Value;
            backgroundPage.PageSheet.PageProps.PageWidth.Value = pageWidth;
            backgroundPage.PageSheet.PageProps.PageHeight.Value = pageHeight;

            // Add a rectangle shape that spans the entire page
            // Master name "Rectangle" is a standard Visio master
            long rectShapeId = backgroundPage.AddShape(
                pinX: pageWidth / 2.0,   // Center X
                pinY: pageHeight / 2.0,  // Center Y
                width: pageWidth,
                height: pageHeight,
                masterName: "Rectangle"); // No pageNumber parameter for Page.AddShape

            // Retrieve the shape object
            Shape rectShape = backgroundPage.Shapes.GetShape(rectShapeId);

            // Set solid fill pattern
            rectShape.Fill.FillPattern.Value = 1; // Solid fill
            // Set desired background color (hex string)
            rectShape.Fill.FillForegnd.Value = "#ADD8E6"; // Light blue

            // Remove outline
            rectShape.Line.LinePattern.Value = 0; // No line

            // Send the shape to back so other content appears above it
            rectShape.SendToBack();

            // Make the background shape non‑selectable
            rectShape.Protection.LockSelect.Value = BOOL.True;

            // Add the background page to the diagram
            diagram.Pages.Add(backgroundPage);

            // Link the foreground page to its background page
            foregroundPage.BackPage = backgroundPage;

            // Prepare PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            // Save the diagram as PDF
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram exported to PDF with custom background: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}