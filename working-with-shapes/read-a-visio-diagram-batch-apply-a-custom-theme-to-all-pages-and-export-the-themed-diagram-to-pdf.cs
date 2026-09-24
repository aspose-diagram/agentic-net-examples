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

            // Paths for input Visio file and output PDF
            string inputPath = "input.vsdx";
            string outputPath = "themed_output.pdf";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Apply a preset theme to every page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Choose a preset theme and its variant
                page.PresetTheme = PresetThemeValue.Bubble;
                page.PresetThemeVariant = PresetThemeVariantValue.Variant1;
            }

            // Configure PDF save options (e.g., default font)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";

            // Export the themed diagram to PDF
            diagram.Save(outputPath, pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
