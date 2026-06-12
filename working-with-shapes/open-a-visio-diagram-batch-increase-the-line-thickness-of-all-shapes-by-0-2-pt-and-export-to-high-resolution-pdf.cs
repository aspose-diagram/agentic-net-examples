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

            // Paths to the source Visio file and the output PDF.
            string inputPath = "input.vsdx";
            string outputPath = "output.pdf";

            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Increment value: 0.2 pt = 0.2/72 inches.
            double incrementInInches = 0.2 / 72.0;

            // Iterate through every page and every shape, increasing line thickness.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has a line weight cell and add the increment.
                    if (shape.Line != null && shape.Line.LineWeight != null)
                    {
                        shape.Line.LineWeight.Value += incrementInInches;
                    }
                }
            }

            // Configure PDF save options for high‑resolution output.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            // Save the modified diagram as a PDF.
            diagram.Save(outputPath, pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
