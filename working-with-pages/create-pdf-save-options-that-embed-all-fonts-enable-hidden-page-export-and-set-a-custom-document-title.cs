using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram (replace with your file path)
                using (Diagram diagram = new Diagram("input.vsdx"))
                {
                    // Set a custom document title
                    diagram.DocumentProps.Title = "Custom Document Title";

                    // Configure PDF save options
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();

                    // Embed all fonts by specifying a default fallback font (Aspose embeds fonts automatically when possible)
                    pdfOptions.DefaultFont = "Arial";

                    // Enable export of hidden pages
                    pdfOptions.ExportHiddenPage = true;

                    // Save the diagram as PDF with the configured options
                    diagram.Save("output.pdf", pdfOptions);
                }

                Console.WriteLine("Diagram exported to PDF successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }