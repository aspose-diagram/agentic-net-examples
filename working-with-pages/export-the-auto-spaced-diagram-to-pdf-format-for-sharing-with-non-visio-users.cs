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

            // Load the Visio diagram from a file (auto‑spaced diagram)
            Diagram diagram = new Diagram("input.vsdx");

            // Configure PDF save options – enable page enlargement to fit the drawing content
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                EnlargePage = true   // ensures the PDF page expands to include all shapes
            };

            // Export the diagram to PDF for sharing with non‑Visio users
            diagram.Save("output.pdf", pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
