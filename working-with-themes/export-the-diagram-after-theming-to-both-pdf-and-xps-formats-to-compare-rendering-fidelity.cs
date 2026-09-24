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
            string sourcePath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(sourcePath);

            // Apply a preset theme to each page
            foreach (Page page in diagram.Pages)
            {
                // Set the theme (write‑only property)
                page.PresetTheme = PresetThemeValue.Bubble;
                // Set a variant for the theme
                page.PresetThemeVariant = PresetThemeVariantValue.Variant1;
            }

            // Export to PDF
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";               // Fallback font
            pdfOptions.ExportHiddenPage = false;            // Do not export hidden pages
            string pdfOutput = "output.pdf";
            diagram.Save(pdfOutput, pdfOptions);

            // Export to XPS
            XPSSaveOptions xpsOptions = new XPSSaveOptions();
            xpsOptions.ExportHiddenPage = false;            // Do not export hidden pages
            string xpsOutput = "output.xps";
            diagram.Save(xpsOutput, xpsOptions);

            Console.WriteLine("Export completed: PDF and XPS files generated.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
