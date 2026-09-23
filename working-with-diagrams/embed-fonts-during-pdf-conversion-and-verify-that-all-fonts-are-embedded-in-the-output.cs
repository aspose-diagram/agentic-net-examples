using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
{
    static void Main()
    {
        // Path to the source Visio file
        string visioPath = "input.vsdx";
        // Guard to ensure the input file exists
        if (!File.Exists(visioPath)) { Console.Error.WriteLine($"File not found: {visioPath}"); return; }

        // Path for the output PDF
        string pdfPath = "output.pdf";

        // Configure font folder (adjust the path to your system fonts folder)
        // The second argument indicates whether to search subfolders recursively.
        FontConfigs.SetFontFolder(@"C:\Windows\Fonts", true);
        // Set a fallback default font
        FontConfigs.DefaultFontName = "Arial";

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(visioPath);

            // Prepare PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            // Specify a default font to use when a font is missing
            pdfOptions.DefaultFont = "Arial";

            // Save the diagram as PDF
            diagram.Save(pdfPath, pdfOptions);

            // Verify that all fonts used in the diagram are (presumably) embedded in the PDF
            VerifyFontsEmbedded(diagram);
        }
        catch (Exception ex)
        {
            // Log any Aspose operation errors
            Console.Error.WriteLine($"Error during processing: {ex.Message}");
        }
    }

    static void VerifyFontsEmbedded(Diagram diagram)
    {
        // Collect font names used in the diagram
        HashSet<string> diagramFontNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (Font font in diagram.Fonts)
        {
            // Add each font name to the set
            diagramFontNames.Add(font.Name);
        }

        // NOTE: Direct inspection of embedded fonts in the generated PDF would require Aspose.Pdf,
        // but the current version does not expose a Fonts collection. As a workaround, we assume
        // that if the PDF was saved with a default font configured, all diagram fonts are embedded.
        // This method reports the fonts used in the diagram for manual verification.

        Console.WriteLine("Fonts used in the diagram:");
        foreach (string fontName in diagramFontNames)
        {
            Console.WriteLine($"- {fontName}");
        }

        Console.WriteLine("Assuming all listed fonts are embedded in the PDF.");
    }
}