using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using System.IO;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio diagram path (first argument or default)
                string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Output files
                string htmlPath = "output.html";
                string pdfPath = "output.pdf";

                // Ensure the input file exists
                if (!File.Exists(diagramPath))
                {
                    throw new FileNotFoundException($"Diagram file not found: {diagramPath}");
                }

                // Load the Visio diagram
                Diagram diagram = new Diagram(diagramPath);

                // Export diagram to HTML
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                // Optional: do not export hidden pages
                htmlOptions.ExportHiddenPage = false;
                diagram.Save(htmlPath, htmlOptions);

                // Convert the generated HTML to PDF using Aspose.Pdf (fully qualified to avoid ambiguity)
                // Load the HTML file
                Aspose.Pdf.HtmlLoadOptions loadOptions = new Aspose.Pdf.HtmlLoadOptions();
                Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(htmlPath, loadOptions);

                // Save as PDF
                pdfDocument.Save(pdfPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }