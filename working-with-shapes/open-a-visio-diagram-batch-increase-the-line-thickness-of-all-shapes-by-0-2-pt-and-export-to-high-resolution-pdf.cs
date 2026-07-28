using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string inputPath = "input.vsdx";
                // Output PDF file path
                string outputPath = "output.pdf";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Increment value for line weight: 0.2 pt = 0.2 / 72 inches
                double incrementInInches = 0.2 / 72.0;

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the LineWeight cell exists
                        if (shape.Line != null && shape.Line.LineWeight != null)
                        {
                            // Increase line thickness
                            shape.Line.LineWeight.Value += incrementInInches;
                        }
                    }
                }

                // Configure PDF save options (high‑resolution defaults)
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial"; // fallback font
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Save the modified diagram as a PDF
                diagram.Save(outputPath, pdfOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }