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
                Console.WriteLine("Usage: DiagramExport <inputVisioPath> <outputPdfPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Apply a uniform line thickness to all connector shapes (1‑D shapes).
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Connectors are 1‑D shapes; filter them.
                    if (shape.OneD)
                    {
                        // Set line weight (thickness) in inches. Example: 0.02 inches (~0.5 mm).
                        shape.Line.LineWeight.Value = 0.02;
                    }
                }
            }

            // Configure PDF save options.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";

            // Save the diagram as PDF.
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram exported to PDF successfully: {outputPath}");
        }
    }