using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or a default path)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the diagram
        Diagram diagram = new Diagram(inputPath);

        // Collect original pages before adding background pages
        List<Page> originalPages = new List<Page>();
        foreach (Page p in diagram.Pages)
        {
            originalPages.Add(p);
        }

        // Determine the current maximum page ID to assign unique IDs to new background pages
        int maxPageId = diagram.Pages.Max(p => p.ID);

        // Create a white background page for each original page
        foreach (Page page in originalPages)
        {
            // Page dimensions
            double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

            // Create background page
            Page bgPage = new Page();
            bgPage.ID = ++maxPageId;
            bgPage.Name = page.Name + "_Background";

            // Center coordinates for the rectangle shape
            double pinX = pageWidth / 2.0;
            double pinY = pageHeight / 2.0;

            // Draw a rectangle that covers the whole page
            long rectShapeId = bgPage.DrawRectangle(pinX, pinY, pageWidth, pageHeight);
            Shape rectShape = bgPage.Shapes.GetShape(rectShapeId);

            // Set solid white fill
            rectShape.Fill.FillPattern.Value = 1;               // Solid fill
            rectShape.Fill.FillForegnd.Value = "#FFFFFF";       // White color

            // Remove outline
            rectShape.Line.LinePattern.Value = 0;               // No line

            // Send shape to back and lock selection
            rectShape.SendToBack();
            rectShape.Protection.LockSelect.Value = BOOL.True;

            // Add background page to diagram
            diagram.Pages.Add(bgPage);

            // Link the original page to its background page
            page.BackPage = bgPage;
        }

        // Prepare output directory
        string outputDir = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "PdfPages");
        Directory.CreateDirectory(outputDir);

        // Export each original page as a separate PDF
        for (int i = 0; i < originalPages.Count; i++)
        {
            Page page = originalPages[i];
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                PageIndex = i,                 // Export only this page
                ExportHiddenPage = false,
                DefaultFont = "Arial"
            };

            string outputFileName = $"{Path.GetFileNameWithoutExtension(inputPath)}_{page.Name}.pdf";
            string outputPath = Path.Combine(outputDir, outputFileName);

            diagram.Save(outputPath, pdfOptions);
            Console.WriteLine($"Saved page '{page.Name}' to '{outputPath}'.");
        }
    }
}
