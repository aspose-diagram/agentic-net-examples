using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Set the global header margin to 0.5 inches
                // HeaderMargin is a Margin type; assign its double value directly.
                diagram.HeaderFooter.HeaderMargin.Value = 0.5;

                // Configure PDF save options (optional settings can be added here)
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Ensure the save format is explicitly set (helps avoid ambiguity)
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Export the diagram to PDF using the configured options
                string outputPath = "output.pdf";
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine("Diagram exported to PDF with header margin set to 0.5 inches.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }