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

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Define the corporate line color (hex format)
            string corporateLineColor = "#1A73E8"; // Example corporate blue

            // Iterate through all pages and shapes to find connectors
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Connectors are 1‑D shapes (OneD == true)
                    if (shape.OneD)
                    {
                        // Apply the corporate line color
                        shape.Line.LineColor.Value = corporateLineColor;
                    }
                }
            }

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            // Save the diagram as PDF with the updated connector colors
            diagram.Save(outputPath, pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
