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

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through pages with an index
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];

                // Apply custom border thickness to every non‑deleted shape on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.False)
                    {
                        // Set line weight (border thickness) in inches
                        shape.Line.LineWeight.Value = 0.02; // example thickness
                    }
                }

                // Prepare PDF save options for the current page only
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    // Ensure a default font is available during PDF rendering
                    DefaultFont = "Arial",
                    // Export only the current page
                    PageIndex = pageIndex,
                    PageCount = 1,
                    // Explicitly set the format tracker (optional but safe)
                    SaveFormat = SaveFileFormat.Pdf
                };

                // Build output file name for the page
                string outputPath = $"Page_{pageIndex + 1}.pdf";

                // Save the diagram (only the specified page) as PDF
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine("Processing completed.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
