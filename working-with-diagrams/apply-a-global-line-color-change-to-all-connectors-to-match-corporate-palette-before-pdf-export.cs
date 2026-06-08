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

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the corporate line color (hex format)
                string corporateLineColor = "#FF0000"; // Example: red

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify connector shapes (1-D shapes)
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

                // Save the diagram as PDF with updated connector colors
                diagram.Save(outputPath, pdfOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }