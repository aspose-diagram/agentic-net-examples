using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        // Input Visio file path
        string inputPath = "input.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output folder for individual PDFs
        string outputDir = "OutputPdfs";
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Calculate the center point for the background rectangle
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Draw a rectangle that spans the entire page
                long bgShapeId = page.DrawRectangle(pinX, pinY, pageWidth, pageHeight);
                Shape bgShape = page.Shapes.GetShape(bgShapeId);

                // Set the rectangle fill to solid white
                bgShape.Fill.FillPattern.Value = 1;          // 1 = solid fill
                bgShape.Fill.FillForegnd.Value = "#FFFFFF"; // white color

                // Remove any border from the background shape
                bgShape.Line.LinePattern.Value = 0; // no line

                // Send the background shape to the back so other content appears on top
                bgShape.SendToBack();

                // Prepare PDF save options to export only the current page
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    PageIndex = page.ID, // export this page
                    PageCount = 1,       // only one page per file
                    ExportHiddenPage = false,
                    DefaultFont = "Arial"
                };

                // Build the output PDF file name (e.g., Page-1.pdf)
                string outputPath = Path.Combine(outputDir, $"Page-{page.ID}.pdf");

                // Save the diagram page as a separate PDF
                diagram.Save(outputPath, pdfOptions);
            }
        }
        catch (Exception ex)
        {
            // Log any Aspose or I/O errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}