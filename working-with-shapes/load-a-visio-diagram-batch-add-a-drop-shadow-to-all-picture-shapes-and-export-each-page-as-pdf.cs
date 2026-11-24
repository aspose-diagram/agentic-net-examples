using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input Visio file path as first argument and optional output folder as second argument.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> [outputFolder]");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args.Length >= 2 ? args[1] : Path.GetDirectoryName(inputPath);

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Guard against missing output folder; create if necessary.
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {outputFolder}. Error: {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page to apply drop shadow to picture shapes.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over each shape on the page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify picture shapes by checking for embedded foreign image data.
                        if (shape.ForeignData != null && shape.ForeignData.Value != null && shape.ForeignData.Value.Length > 0)
                        {
                            // Apply a simple drop shadow.
                            shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;
                            shape.Fill.ShdwForegnd.Value = "#000000";          // Shadow color: black.
                            shape.Fill.ShdwForegndTrans.Value = 0.3;          // 30% transparency.
                        }
                    }
                }

                // Export each page as an individual PDF file.
                int pageIndex = 0;
                foreach (Page page in diagram.Pages)
                {
                    string outputPdfPath = Path.Combine(outputFolder, $"Page_{pageIndex + 1}.pdf");

                    // Configure PDF save options for a single page.
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();
                    pdfOptions.SaveFormat = SaveFileFormat.Pdf;   // Explicitly set the format.
                    pdfOptions.PageIndex = pageIndex;            // Zero‑based page index.
                    pdfOptions.PageCount = 1;                    // Export only this page.
                    pdfOptions.ExportHiddenPage = false;         // Do not export hidden pages.

                    // Save the diagram (single page) to PDF.
                    diagram.Save(outputPdfPath, pdfOptions);

                    pageIndex++;
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}