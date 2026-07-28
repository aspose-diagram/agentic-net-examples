using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output PDF file path.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: VisioBatchProcessing <inputVisioPath> <outputPdfPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape contains any text.
                    if (shape.Text != null && !string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                    {
                        // Set the font size for each character run to 10 points (10/72 inches).
                        foreach (Aspose.Diagram.Char ch in shape.Chars)
                        {
                            ch.Size.Value = 10.0 / 72.0;
                        }
                    }
                }
            }

            // Configure PDF save options (optional: set a default font to avoid missing font issues).
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";

            // Save the modified diagram as PDF.
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram processed and saved to PDF at: {outputPath}");
        }
    }