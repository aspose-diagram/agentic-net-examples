using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file (default to "input.vsdx" if not provided)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output files
        string htmlPath = "output.html";
        string pdfPath = "output.pdf";

        // Export diagram to HTML
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions
                {
                    DefaultFont = "Arial"
                };
                diagram.Save(htmlPath, htmlOptions);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error exporting to HTML: {ex.Message}");
            return;
        }

        // Ensure the HTML file exists before proceeding
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file was not created: {htmlPath}");
            return;
        }

        // Export diagram to PDF (snapshot)
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    DefaultFont = "Arial"
                };
                diagram.Save(pdfPath, pdfOptions);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error exporting to PDF: {ex.Message}");
            return;
        }

        Console.WriteLine($"Diagram converted to HTML: {Path.GetFullPath(htmlPath)}");
        Console.WriteLine($"PDF snapshot created: {Path.GetFullPath(pdfPath)}");
    }
}