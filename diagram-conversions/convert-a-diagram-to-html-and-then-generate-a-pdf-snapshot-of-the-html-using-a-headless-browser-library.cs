using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio diagram file path
                string diagramPath = "input.vsdx";

                // Verify the diagram file exists
                if (!File.Exists(diagramPath))
                    throw new FileNotFoundException($"Diagram file not found: {diagramPath}");

                // Load the Visio diagram
                Diagram diagram = new Diagram(diagramPath);

                // Export the diagram to HTML
                string htmlPath = "output.html";
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                diagram.Save(htmlPath, htmlOptions);

                // Convert the generated HTML to PDF using Aspose.Pdf (fully qualified to avoid namespace conflict)
                string pdfPath = "output.pdf";
                var htmlLoadOptions = new Aspose.Pdf.HtmlLoadOptions();
                Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(htmlPath, htmlLoadOptions);
                pdfDocument.Save(pdfPath);

                Console.WriteLine("Conversion completed successfully.");
                Console.WriteLine($"HTML file: {Path.GetFullPath(htmlPath)}");
                Console.WriteLine($"PDF file: {Path.GetFullPath(pdfPath)}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }