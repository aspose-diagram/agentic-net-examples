using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";

                // Output PDF file path
                string outputPath = "output.pdf";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Set line pattern to dashed
                        shape.Line.LinePattern.Value = LinePatternValue.Dash;
                    }
                }

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";
                pdfOptions.SaveFormat = SaveFileFormat.Pdf; // Explicitly set format tracker

                // Save the modified diagram as PDF
                diagram.Save(outputPath, pdfOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }