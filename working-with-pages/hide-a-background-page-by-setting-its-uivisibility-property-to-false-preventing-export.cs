using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = "output.pdf";

        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                foreach (Page page in diagram.Pages)
                {
                    if (page.Background == BOOL.True)
                    {
                        page.PageSheet.PageProps.UIVisibility.Value = UIVisibilityValue.Hidden;
                    }
                }

                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.ExportHiddenPage = false;
                pdfOptions.DefaultFont = "Arial";

                diagram.Save(outputPath, pdfOptions);
            }

            Console.WriteLine("Diagram processed and saved to PDF without hidden pages.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}