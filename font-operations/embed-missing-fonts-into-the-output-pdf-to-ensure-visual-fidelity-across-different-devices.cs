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

            // Load the Visio diagram from file
            Diagram diagram = new Diagram("input.vsdx");

            // Create PDF save options and set a fallback font.
            // This font will be used when the original font is missing,
            // ensuring characters are rendered correctly in the PDF.
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial Unicode MS";

            // Optionally define font substitutes for specific fonts that may be missing.
            // If "Times New Roman" is not available, the substitutes will be tried in order.
            FontConfigs.SetFontSubstitutes("Times New Roman", new[] { "Liberation Serif", "Arial Unicode MS" });

            // Save the diagram as PDF using the configured options.
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
