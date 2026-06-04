using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class PageProgressCallback : IPageSavingCallback
{
    private readonly Diagram _diagram;
    private readonly string _outputFolder;

    public PageProgressCallback(Diagram diagram, string outputFolder)
    {
        _diagram = diagram;
        _outputFolder = outputFolder;
    }

    public void PageStartSaving(PageStartSavingArgs args)
    {
        Console.WriteLine($"Start saving page {args.PageIndex + 1} of {args.PageCount}");
        Directory.CreateDirectory(_outputFolder);
        var imgOptions = new ImageSaveOptions(SaveFileFormat.Png)
        {
            PageIndex = args.PageIndex,
            ExportHiddenPage = false
        };
        string pngPath = Path.Combine(_outputFolder, $"Page_{args.PageIndex + 1}.png");
        _diagram.Save(pngPath, imgOptions);
    }

    public void PageEndSaving(PageEndSavingArgs args)
    {
        Console.WriteLine($"Finished saving page {args.PageIndex + 1}");
        // Optional: stop further processing
        // args.HasMorePages = false;
    }
}

class Program
{
    static void Main()
    {
        try
        {

            string sourcePath = "input.vsdx";
            string outputFolder = "PageImages";

            using (var diagram = new Diagram(sourcePath))
            {
                var callback = new PageProgressCallback(diagram, outputFolder);
                var pdfOptions = new PdfSaveOptions
                {
                    DefaultFont = "Arial",
                    ExportHiddenPage = false,
                    PageSavingCallback = callback
                };
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                string tempPdf = Path.Combine(outputFolder, "temp.pdf");
                diagram.Save(tempPdf, pdfOptions);
            }

            Console.WriteLine("All pages have been exported as PNG files.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}