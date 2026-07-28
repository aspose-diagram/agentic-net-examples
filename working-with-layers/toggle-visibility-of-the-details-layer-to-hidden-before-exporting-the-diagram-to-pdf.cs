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

            // Input Visio file path
            string inputPath = "input.vsdx";
            // Output PDF file path
            string outputPath = "output.pdf";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages to find the 'Details' layer
                foreach (Page page in diagram.Pages)
                {
                    // Access the layer collection of the page
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // Compare layer name (case-sensitive)
                        if (layer.Name.Value == "Details")
                        {
                            // Hide the layer
                            layer.Visible.Value = BOOL.False;
                        }
                    }
                }

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    // Ensure hidden pages/layers are not exported
                    ExportHiddenPage = false
                };

                // Export the diagram to PDF
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine("Diagram exported to PDF with 'Details' layer hidden.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
