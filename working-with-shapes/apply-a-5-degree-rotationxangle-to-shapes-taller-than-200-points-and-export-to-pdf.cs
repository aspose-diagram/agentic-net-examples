using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output PDF file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramRotationExport <inputVisioFile> <outputPdfFile>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Threshold: 200 points = 200/72 inches
            double heightThresholdInches = 200.0 / 72.0;

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Check shape height
                    double shapeHeight = shape.XForm.Height.Value;
                    if (shapeHeight > heightThresholdInches)
                    {
                        // Apply 5-degree rotation around X axis
                        shape.ThreeDFormat.RotationXAngle.Value = 5.0;
                    }
                }
            }

            // Prepare PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            // Optional: set a default font to avoid missing font warnings
            pdfOptions.DefaultFont = "Arial";

            // Save the diagram as PDF
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram processed and saved to PDF: {outputPath}");
        }
    }