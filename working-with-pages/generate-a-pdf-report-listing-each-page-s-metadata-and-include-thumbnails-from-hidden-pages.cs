using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Paths for source Visio file and the generated PDF report
        string sourcePath = "input.vsdx";
        if (!File.Exists(sourcePath)) { Console.Error.WriteLine($"File not found: {sourcePath}"); return; }
        string reportPdfPath = "Report.pdf";

        try
        {
            // Load the source diagram
            using (Diagram sourceDiagram = new Diagram(sourcePath))
            {
                // Create a new empty diagram that will hold the report
                Diagram reportDiagram = new Diagram();

                // Add a single page to the report diagram
                Page reportPage = new Page();
                reportDiagram.Pages.Add(reportPage);

                // Layout variables for placing text and thumbnail images
                double startX = 0.5;          // left margin
                double startY = 0.5;          // top margin
                double lineHeight = 1.2;      // vertical space per entry
                double thumbX = 6.0;          // thumbnail left position
                double thumbWidth = 1.5;      // thumbnail width
                double thumbHeight = 1.5;     // thumbnail height

                // Iterate through each page of the source diagram
                for (int i = 0; i < sourceDiagram.Pages.Count; i++)
                {
                    Page srcPage = sourceDiagram.Pages[i];

                    // Gather page metadata
                    long pageId = srcPage.ID;
                    string pageName = srcPage.Name ?? "";
                    string pageNameU = srcPage.NameU ?? "";
                    bool isBackground = srcPage.Background == Aspose.Diagram.BOOL.True;
                    double pageWidth = srcPage.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = srcPage.PageSheet.PageProps.PageHeight.Value;

                    // Build a metadata string
                    string meta = $"Page {i + 1}:" +
                                  $" ID={pageId}," +
                                  $" Name=\"{pageName}\"," +
                                  $" NameU=\"{pageNameU}\"," +
                                  $" Background={isBackground}," +
                                  $" Size={pageWidth:F2}in x {pageHeight:F2}in";

                    // Add a text shape with the metadata
                    double textY = startY + i * lineHeight;
                    reportPage.AddText(startX, textY, 5.0, lineHeight, meta);
                }

                // Save the report diagram as PDF
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                reportDiagram.Save(reportPdfPath, pdfOptions);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}