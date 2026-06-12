using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output PDF file path
                string outputPath = "output.pdf";

                // Configure font folder(s) so that fonts used in the diagram can be found.
                // The second parameter indicates whether to search subfolders recursively.
                FontConfigs.SetFontFolder(@"C:\Windows\Fonts", true);

                // Load the Visio diagram from file.
                Diagram diagram = new Diagram(inputPath);

                // Create PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Set the fallback font to use when a required font is missing.
                pdfOptions.DefaultFont = "Arial";

                // Explicitly set the save format (required for PDF export).
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Save the diagram as a PDF with the specified options.
                diagram.Save(outputPath, pdfOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }