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

            // Path for the resulting PDF file
            string outputPath = "output.pdf";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Set up PDF save options to include hidden pages
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.ExportHiddenPage = true;

            // Save the diagram as PDF using the configured options
            diagram.Save(outputPath, pdfOptions);

            // Clean up resources
            diagram.Dispose();

            Console.WriteLine("PDF saved with hidden pages included.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
