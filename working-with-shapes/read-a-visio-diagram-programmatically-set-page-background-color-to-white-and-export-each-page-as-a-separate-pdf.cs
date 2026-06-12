using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (provide as first argument or modify the string)
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page to set a white background
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Center coordinates for the rectangle shape
                    double pinX = pageWidth / 2.0;
                    double pinY = pageHeight / 2.0;

                    // Draw a rectangle that covers the entire page
                    long rectShapeId = page.DrawRectangle(pinX, pinY, pageWidth, pageHeight);
                    Shape rectShape = page.Shapes.GetShape((int)rectShapeId);

                    // Set solid white fill
                    rectShape.Fill.FillPattern.Value = 1;               // Solid fill
                    rectShape.Fill.FillForegnd.Value = "#FFFFFF";       // White color

                    // Remove outline
                    rectShape.Line.LinePattern.Value = 0;               // No line

                    // Send the rectangle to the back so it acts as a background
                    page.SendToBack(rectShapeId);

                    // Make the background shape non‑selectable
                    rectShape.Protection.LockSelect.Value = BOOL.True;
                }

                // Export each page as a separate PDF
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    // Configure PDF save options for a single page
                    PdfSaveOptions pdfOptions = new PdfSaveOptions
                    {
                        ExportHiddenPage = false,
                        PageIndex = i,      // Zero‑based page index
                        PageCount = 1,      // Export only one page
                        DefaultFont = "Arial"
                    };

                    // Build output file name
                    string baseName = Path.GetFileNameWithoutExtension(inputPath);
                    string outputPath = $"{baseName}_Page{i + 1}.pdf";

                    // Save the diagram (only the specified page) as PDF
                    diagram.Save(outputPath, pdfOptions);
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }