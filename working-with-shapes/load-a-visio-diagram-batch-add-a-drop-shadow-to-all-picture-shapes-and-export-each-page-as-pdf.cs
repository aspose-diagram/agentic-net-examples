using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output folder path.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: VisioShadowExport <inputVisioPath> <outputFolder>");
                return;
            }

            string inputPath = args[0];
            string outputFolder = args[1];

            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"Input file not found: {inputPath}");
                return;
            }

            if (!Directory.Exists(outputFolder))
            {
                Console.WriteLine($"Output folder does not exist. Creating: {outputFolder}");
                Directory.CreateDirectory(outputFolder);
            }

            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Identify picture (foreign) shapes.
                    if (shape.Type == TypeValue.Foreign)
                    {
                        // Apply a simple drop shadow.
                        shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;
                        shape.Fill.ShdwForegnd.Value = "#000000";          // Shadow color: black
                        shape.Fill.ShdwForegndTrans.Value = 0.3;           // 30% transparency
                        shape.Fill.ShapeShdwOffsetX.Value = 0.1;           // Horizontal offset (in inches)
                        shape.Fill.ShapeShdwOffsetY.Value = 0.1;           // Vertical offset (in inches)
                    }
                }

                // Prepare PDF save options for the current page.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;
                pdfOptions.DefaultFont = "Arial";
                pdfOptions.ExportHiddenPage = false;
                pdfOptions.PageIndex = page.ID - 1; // Zero‑based page index.
                pdfOptions.PageCount = 1;           // Export only this page.

                // Build output file name using the page name.
                string safePageName = string.IsNullOrWhiteSpace(page.Name) ? $"Page_{page.ID}" : page.Name;
                string outputPath = Path.Combine(outputFolder, $"{safePageName}.pdf");

                // Save the single page as PDF.
                diagram.Save(outputPath, pdfOptions);
                Console.WriteLine($"Exported page '{safePageName}' to PDF: {outputPath}");
            }
        }
    }