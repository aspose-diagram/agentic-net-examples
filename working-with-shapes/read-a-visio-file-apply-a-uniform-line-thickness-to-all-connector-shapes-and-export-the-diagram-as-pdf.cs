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

            // Desired uniform line thickness (in inches)
            double uniformThickness = 0.02; // Example: 0.02 inches (~0.5 mm)

            // Apply the line thickness to all connector (1‑D) shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Connectors are identified by the OneD property
                    if (shape.OneD)
                    {
                        shape.Line.LineWeight.Value = uniformThickness;
                    }
                }
            }

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            // Export the diagram as PDF
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine($"Diagram successfully saved as PDF to: {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
