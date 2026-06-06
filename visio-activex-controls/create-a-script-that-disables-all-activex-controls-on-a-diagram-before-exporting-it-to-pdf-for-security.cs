using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.ActiveXControls;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output PDF file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramSecurityExport <inputVisioFile> <outputPdfFile>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            try
            {
                // Load the diagram from the specified file
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes to disable ActiveX controls
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // If the shape contains an ActiveX control, mark the shape as deleted
                        if (shape.ActiveXControl != null)
                        {
                            shape.Del = BOOL.True;
                        }
                    }
                }

                // Prepare PDF save options (optional: set default font to avoid missing glyphs)
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    DefaultFont = "Arial"
                };

                // Export the modified diagram to PDF
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine($"Diagram exported successfully to PDF: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing diagram: {ex.Message}");
                throw;
            }
        }
    }