using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths for input Visio diagram, intermediate HTML, and final PDF.
                string diagramPath = "input.vsdx";
                string htmlPath = "output.html";
                string pdfPath = "output.pdf";

                // Load the Visio diagram.
                Diagram diagram = new Diagram(diagramPath);

                // Export the diagram to HTML.
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                diagram.Save(htmlPath, htmlOptions);

                // Convert the generated HTML to PDF using Aspose.Pdf.
                // Types from Aspose.Pdf are fully qualified to avoid namespace conflicts.
                var pdfDocument = new Aspose.Pdf.Document(htmlPath, new Aspose.Pdf.HtmlLoadOptions());
                pdfDocument.Save(pdfPath);

                // Clean up resources.
                diagram.Dispose();

                Console.WriteLine("Diagram successfully converted to HTML and PDF.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }