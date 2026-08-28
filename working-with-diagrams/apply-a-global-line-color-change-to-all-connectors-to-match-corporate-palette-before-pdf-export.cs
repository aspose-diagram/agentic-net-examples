using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output PDF file path
        string outputPath = "output.pdf";

        try
        {
            // Load the diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Define the corporate line color (hex format)
            const string corporateLineColor = "#00ADEF";

            // Iterate through all pages and shapes to update connector line colors
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify connector shapes (1‑D shapes)
                    if (shape.OneD)
                    {
                        // Apply the corporate line color to the connector's line
                        shape.Line.LineColor.Value = corporateLineColor;
                    }
                }
            }

            // Configure PDF save options (no AutoFitPageToDrawingContent property)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;

            // Save the diagram as PDF with the updated connector colors
            diagram.Save(outputPath, pdfOptions);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}