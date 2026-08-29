using System.IO;
using System;
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
        string outputPath = args[1];

        // Load the Visio diagram
        Diagram diagram = new Diagram(inputPath);

        // Determine the maximum existing page ID to assign unique IDs to new background pages
        int maxPageId = 0;
        foreach (Page p in diagram.Pages)
        {
            if (p.ID > maxPageId)
                maxPageId = p.ID;
        }

        // Process each foreground page
        foreach (Page page in diagram.Pages)
        {
            // Skip if the page is already a background page
            if (page.Background == BOOL.True)
                continue;

            // Create a new background page
            Page bgPage = new Page();
            bgPage.ID = ++maxPageId;
            bgPage.Name = $"Background_{bgPage.ID}";
            bgPage.Background = BOOL.True;

            // Retrieve dimensions of the foreground page
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Add a rectangle shape that covers the entire page
            // PinX and PinY are the center of the shape
            double pinX = pageWidth / 2.0;
            double pinY = pageHeight / 2.0;
            long rectShapeId = bgPage.AddShape(pinX, pinY, pageWidth, pageHeight, "Rectangle");
            Shape rectShape = bgPage.Shapes.GetShape(rectShapeId);

            // Set solid fill with light gray color (#D3D3D3)
            rectShape.Fill.FillPattern.Value = 1;               // Solid fill
            rectShape.Fill.FillForegnd.Value = "#D3D3D3";       // Light gray

            // Remove border
            rectShape.Line.LinePattern.Value = 0;               // No line

            // Send the rectangle to the back and lock selection
            rectShape.SendToBack();
            rectShape.Protection.LockSelect.Value = BOOL.True;

            // Add the background page to the diagram
            diagram.Pages.Add(bgPage);

            // Associate the foreground page with its background page
            page.BackPage = bgPage;
        }

        // Configure PDF save options
        PdfSaveOptions pdfOptions = new PdfSaveOptions();
        pdfOptions.DefaultFont = "Arial";

        // Save the modified diagram as PDF
        diagram.Save(outputPath, pdfOptions);

        Console.WriteLine($"Diagram processed and saved to PDF: {outputPath}");
    }
}
