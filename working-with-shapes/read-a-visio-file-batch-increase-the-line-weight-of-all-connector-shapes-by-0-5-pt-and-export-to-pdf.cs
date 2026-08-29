using System.IO;
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

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Increment value: 0.5 point = 0.5/72 inches
            double incrementInInches = 0.5 / 72.0;

            // Iterate over all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Process only connector (1‑D) shapes that are not deleted
                    if (shape.OneD && shape.Del == BOOL.False)
                    {
                        // Increase line weight
                        shape.Line.LineWeight.Value += incrementInInches;
                    }
                }
            }

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            // Save the modified diagram as PDF
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram exported to PDF: {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
