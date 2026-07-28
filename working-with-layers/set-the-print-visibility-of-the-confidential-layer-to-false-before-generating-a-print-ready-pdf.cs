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
                // Iterate through all pages to locate the 'Confidential' layer
                foreach (Page page in diagram.Pages)
                {
                    // Access the layer collection of the current page
                    foreach (Layer layer in page.PageSheet.Layers)
                    {
                        // Compare layer name (use .Value to get the string)
                        if (layer.Name.Value == "Confidential")
                        {
                            // Set the print visibility to false
                            layer.Print.Value = BOOL.False;
                        }
                    }
                }

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Save the diagram as a PDF
                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine("PDF generated with 'Confidential' layer hidden from printing.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
