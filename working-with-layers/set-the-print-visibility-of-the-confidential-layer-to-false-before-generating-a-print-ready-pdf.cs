using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Printing;

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
                // Iterate through all pages to locate the "Confidential" layer
                foreach (Page page in diagram.Pages)
                {
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // Compare layer name (Str2Value) with the target name
                        if (layer.Name.Value == "Confidential")
                        {
                            // Set the layer's print visibility to false
                            layer.Print.Value = BOOL.False;
                        }
                    }
                }

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;
                // Optional: exclude hidden pages from the PDF
                pdfOptions.ExportHiddenPage = false;

                // Save the diagram as a PDF
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine("PDF generated successfully with 'Confidential' layer hidden from printing.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
