using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Paths to the input Visio file and the output PDF file
            string inputPath = "input.vsdx";
            string outputPath = "output.pdf";

            try
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Add page numbers to the footer (Visio field code &p inserts the current page number)
                diagram.HeaderFooter.FooterRight = "Page: &p";

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";
                pdfOptions.SaveFormat = SaveFileFormat.Pdf; // Explicitly set the format

                // Export the diagram to PDF
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine("Export completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                throw;
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
