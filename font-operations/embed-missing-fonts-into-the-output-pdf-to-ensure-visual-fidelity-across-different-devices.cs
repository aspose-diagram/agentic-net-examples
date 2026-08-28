using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the source Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Set a global default font to be used when the original font is missing
            FontConfigs.DefaultFontName = "Arial";

            // Define font substitutes for specific fonts that might be absent on the target system
            FontConfigs.SetFontSubstitutes("Times New Roman", new string[] { "Liberation Serif", "Arial" });

            // Configure PDF save options, including the fallback font
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";               // Fallback font for missing glyphs
            pdfOptions.Compliance = PdfCompliance.Pdf15;    // Optional: set PDF compliance level
            pdfOptions.TextCompression = PdfTextCompression.Flate; // Optional: compress PDF text streams

            // Save the diagram as PDF using the configured options
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
