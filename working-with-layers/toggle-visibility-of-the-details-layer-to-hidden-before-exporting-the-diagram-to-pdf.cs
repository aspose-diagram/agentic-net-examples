using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input Visio file and output PDF file paths as arguments
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: <inputVisioFile> <outputPdfFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Load the diagram from the specified file
        Diagram diagram = new Diagram(inputPath);

        // Hide the layer named "Details" on every page
        foreach (Page page in diagram.Pages)
        {
            foreach (Layer layer in page.PageSheet.Layers)
            {
                if (layer.Name.Value == "Details")
                {
                    layer.Visible.Value = BOOL.False;
                }
            }
        }

        // Configure PDF save options to exclude hidden pages/layers
        PdfSaveOptions pdfOptions = new PdfSaveOptions
        {
            ExportHiddenPage = false,
            DefaultFont = "Arial"
        };

        // Export the diagram to PDF
        diagram.Save(outputPath, pdfOptions);
    }
}
