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
            string inputPath = "input.vsdx";
            // Path for the generated PDF
            string outputPath = "output.pdf";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages and their layers
                foreach (Page page in diagram.Pages)
                {
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // Find the layer named "Confidential"
                        if (layer.Name.Value == "Confidential")
                        {
                            // Set the print visibility to false
                            layer.Print.Value = BOOL.False;
                        }
                    }
                }

                // Configure PDF save options (optional settings)
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.ExportHiddenPage = false; // Do not export hidden pages
                pdfOptions.SaveFormat = SaveFileFormat.Pdf; // Explicitly set format

                // Save the diagram as a PDF
                diagram.Save(outputPath, pdfOptions);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
