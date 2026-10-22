using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Verify the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Output directory for PDF pages (second argument or current directory)
        string outputDir = args.Length > 1 ? args[1] : Directory.GetCurrentDirectory();

        // Verify the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block
            using (Diagram diagram = new Diagram(inputPath))
            {
                int pageIndex = 0;

                // Iterate over each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify picture shapes by checking for embedded foreign data (image bytes)
                        if (shape.ForeignData != null && shape.ForeignData.Value != null && shape.ForeignData.Value.Length > 0)
                        {
                            // Apply a simple drop shadow to the picture shape using cell-based APIs
                            shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;   // Shadow type
                            shape.Fill.ShdwForegnd.Value = "#000000";                     // Shadow color (black)
                            shape.Fill.ShdwForegndTrans.Value = 0.3;                     // 30% transparency
                        }
                    }

                    // Build the output PDF file name for the current page
                    string pdfPath = Path.Combine(outputDir, $"Page_{pageIndex + 1}.pdf");

                    // Configure PDF save options to export only the current page
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();
                    pdfOptions.PageIndex = pageIndex;   // Zero‑based page index
                    pdfOptions.PageCount = 1;           // Export a single page

                    // Save the diagram page as a PDF using the configured options
                    diagram.Save(pdfPath, pdfOptions);

                    pageIndex++;
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}