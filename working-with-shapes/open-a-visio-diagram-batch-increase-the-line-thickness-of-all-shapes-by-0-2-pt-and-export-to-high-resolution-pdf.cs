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

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path for the exported PDF
            string outputPath = "output.pdf";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Conversion factor: points to inches (1 pt = 1/72 inch)
            double pointToInch = 1.0 / 72.0;
            // Increment of 0.2 pt expressed in inches
            double increment = 0.2 * pointToInch;

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Increase line thickness (LineWeight) by the increment
                    // Ensure the shape has a line weight cell; default to 0 if null
                    double currentWeight = shape.Line.LineWeight.Value;
                    shape.Line.LineWeight.Value = currentWeight + increment;
                }
            }

            // Configure PDF save options for high‑resolution output
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";               // Fallback font
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;     // Explicitly set format

            // Save the modified diagram as a PDF
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine("Diagram processed and saved to PDF successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
