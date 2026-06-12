using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (first argument or default)
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Output PDF file path (second argument or default)
                string outputPath = args.Length > 1 ? args[1] : "output.pdf";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Set the footer to display the current date using Visio field code &d
                diagram.HeaderFooter.FooterRight = "&d";

                // Optional: adjust footer margin (in inches) if needed
                // diagram.HeaderFooter.FooterMargin.Value = 0.2;

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    // Ensure a default font is available for characters without a matching font
                    DefaultFont = "Arial",
                    // Explicitly set the save format (required when using PdfSaveOptions)
                    SaveFormat = SaveFileFormat.Pdf
                };

                // Save the diagram as PDF with the specified options
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine($"Diagram saved as PDF with footer date to: {outputPath}");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }