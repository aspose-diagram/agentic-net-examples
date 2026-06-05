using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class UnicodeFallbackExample
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Define fallback fonts for fonts that may be missing on the target system.
            // When a character is not available in the original font, Aspose.Diagram will try the substitutes.
            FontConfigs.SetFontSubstitutes("Arial", new string[] { "Liberation Sans", "Noto Sans" });
            FontConfigs.SetFontSubstitutes("Times New Roman", new string[] { "Noto Serif", "Liberation Serif" });

            // Set a default font for Unicode characters that have no explicit font mapping.
            // This helps avoid missing glyph blocks in the exported document.
            PdfSaveOptions saveOptions = new PdfSaveOptions
            {
                DefaultFont = "MS Gothic" // Choose a font that contains the required Unicode glyphs.
            };

            // Save the diagram to PDF (or any other supported format) using the configured options.
            string outputPath = "output.pdf";
            diagram.Save(outputPath, saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
