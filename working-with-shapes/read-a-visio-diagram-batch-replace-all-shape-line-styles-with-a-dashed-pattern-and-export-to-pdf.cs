using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file path (first argument) or default.
            string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
            // Output PDF file path (second argument) or default.
            string outputPath = args.Length > 1 ? args[1] : "output.pdf";

            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to set a dashed line pattern.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Apply dashed line style.
                    shape.Line.LinePattern.Value = LinePatternValue.Dash;
                }
            }

            // Configure PDF save options.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            // Save the modified diagram as PDF.
            diagram.Save(outputPath, pdfOptions);

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
