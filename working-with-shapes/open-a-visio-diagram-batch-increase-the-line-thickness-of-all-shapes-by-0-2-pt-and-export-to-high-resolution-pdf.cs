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

            // Paths to the source Visio file and the output PDF
            string inputPath = "input.vsdx";
            string outputPath = "output.pdf";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Increment to add to each shape's line weight (0.2 pt = 0.2/72 inches)
            double incrementInches = 0.2 / 72.0;

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Increase line thickness
                    shape.Line.LineWeight.Value += incrementInches;
                }
            }

            // Configure PDF save options (default font set for safety)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";

            // Save the modified diagram as a high‑resolution PDF
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine("Diagram processing complete. PDF saved to: " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
