using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Define corporate standard margins (in inches)
                double topMargin = 0.5;    // 0.5 inch
                double bottomMargin = 0.5; // 0.5 inch
                double leftMargin = 0.5;   // 0.5 inch
                double rightMargin = 0.5;  // 0.5 inch

                // Apply margins to every page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Access the PrintProps collection and set margin values
                    page.PageSheet.PrintProps.PageTopMargin.Value = topMargin;
                    page.PageSheet.PrintProps.PageBottomMargin.Value = bottomMargin;
                    page.PageSheet.PrintProps.PageLeftMargin.Value = leftMargin;
                    page.PageSheet.PrintProps.PageRightMargin.Value = rightMargin;
                }

                // Configure PDF save options (optional: set default font for missing fonts)
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";

                // Save the diagram as PDF with the configured margins
                string outputPath = "output.pdf";
                diagram.Save(outputPath, pdfOptions);

                // Clean up resources
                diagram.Dispose();

                Console.WriteLine("PDF exported successfully with custom margins.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }