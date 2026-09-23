using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class Program
{
    public static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path for the exported PDF
            string outputPath = "output.pdf";

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();

                // Export hidden pages so they are included in the PDF (archival purpose)
                pdfOptions.ExportHiddenPage = true;

                // Ensure the options know they are for PDF format
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Apply text compression (Flate) to reduce file size
                pdfOptions.TextCompression = PdfTextCompression.Flate;

                // Save the diagram as PDF with the configured options
                diagram.Save(outputPath, pdfOptions);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
