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
                Console.WriteLine("Usage: DiagramRotationExample <inputVisioPath> <outputPdfPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Rotate each non-deleted shape on every page by 45 degrees.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked for deletion.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Set rotation angle (degrees).
                    shape.XForm.Angle.Value = 45;
                }
            }

            // Configure PDF save options.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            // Save the modified diagram as PDF.
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram rotated and saved to PDF: {outputPath}");
        }
    }