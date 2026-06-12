using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output PDF file path
                string outputPath = "output.pdf";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Get the first page
                Page page = diagram.Pages[0];

                // Find the first non‑deleted shape on the page
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Del == BOOL.False)
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    throw new Exception("No visible shape found on the first page.");
                }

                // Adjust the perspective angle to 30 degrees
                // ThreeDFormat.Perspective is a double value representing the perspective depth
                targetShape.ThreeDFormat.Perspective.Value = 30;

                // Prepare PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    // Optional: set a fallback font in case the diagram uses missing fonts
                    DefaultFont = "Arial"
                };

                // Export the diagram to PDF
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine($"Diagram exported to PDF with perspective set to 30 degrees: {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }