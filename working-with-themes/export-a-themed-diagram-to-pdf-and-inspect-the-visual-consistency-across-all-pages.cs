using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramExportExample
{
    // Callback to log page saving events
    public class PageSavingLogger : IPageSavingCallback
    {
        public void PageStartSaving(PageStartSavingArgs args)
        {
            Console.WriteLine($"Starting to save page {args.PageIndex + 1} of {args.PageCount}.");
        }

        public void PageEndSaving(PageEndSavingArgs args)
        {
            Console.WriteLine($"Finished saving page {args.PageIndex + 1}.");
        }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio diagram
                string inputPath = "input.vsdx";
                // Path for the exported PDF
                string outputPdfPath = "output.pdf";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Apply a preset theme to every page
                foreach (Page page in diagram.Pages)
                {
                    page.PresetTheme = PresetThemeValue.Bubble;
                }

                // Configure PDF save options with a page-saving callback
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";
                pdfOptions.PageSavingCallback = new PageSavingLogger();
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Export the diagram to PDF
                diagram.Save(outputPdfPath, pdfOptions);
                Console.WriteLine($"Diagram exported to PDF at: {outputPdfPath}");

                // Inspect visual consistency across all pages
                double? referenceWidth = null;
                double? referenceHeight = null;
                int? referenceShapeCount = null;

                int pageNumber = 0;
                foreach (Page page in diagram.Pages)
                {
                    pageNumber++;

                    double width = page.PageSheet.PageProps.PageWidth.Value;
                    double height = page.PageSheet.PageProps.PageHeight.Value;
                    int shapeCount = page.Shapes.Count;

                    Console.WriteLine($"Page {pageNumber}: Width={width}in, Height={height}in, Shapes={shapeCount}");

                    if (referenceWidth == null)
                    {
                        referenceWidth = width;
                        referenceHeight = height;
                        referenceShapeCount = shapeCount;
                    }
                    else
                    {
                        if (Math.Abs(width - referenceWidth.Value) > 0.001 ||
                            Math.Abs(height - referenceHeight.Value) > 0.001)
                        {
                            throw new Exception($"Page {pageNumber} dimensions differ from the first page.");
                        }

                        if (shapeCount != referenceShapeCount.Value)
                        {
                            throw new Exception($"Page {pageNumber} shape count ({shapeCount}) differs from the first page ({referenceShapeCount.Value}).");
                        }
                    }
                }

                Console.WriteLine("Visual consistency check passed for all pages.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}