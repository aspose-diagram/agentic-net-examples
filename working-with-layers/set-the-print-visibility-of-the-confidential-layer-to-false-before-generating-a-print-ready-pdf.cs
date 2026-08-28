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

            // Paths to the source Visio file and the output PDF
            string inputPath = "input.vsdx";
            string outputPath = "output.pdf";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Set print visibility of the "Confidential" layer to false on every page
            foreach (Page page in diagram.Pages)
            {
                foreach (Layer layer in page.PageSheet.Layers)
                {
                    if (layer.Name.Value == "Confidential")
                    {
                        layer.Print.Value = BOOL.False;
                    }
                }
            }

            // Configure PDF save options
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.SaveFormat = SaveFileFormat.Pdf;
            pdfOptions.ExportHiddenPage = false;

            // Save the diagram as a PDF
            diagram.Save(outputPath, pdfOptions);

            // Clean up
            diagram.Dispose();

            Console.WriteLine("Print‑ready PDF generated successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
