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
                Console.WriteLine("Usage: VisioConnectorWeightUpdater <inputVisioPath> <outputPdfPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Increment value: 0.5 point = 0.5 / 72 inches.
            double incrementInInches = 0.5 / 72.0;

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify connector shapes (1‑D shapes).
                    if (shape.OneD)
                    {
                        // Increase line weight by the specified increment.
                        shape.Line.LineWeight.Value += incrementInInches;
                    }
                }
            }

            // Configure PDF save options.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;
            // Optional: set a default font to avoid missing‑font issues.
            pdfOptions.DefaultFont = "Arial";

            // Save the modified diagram as PDF.
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram processed and saved to PDF: {outputPath}");
        }
    }