using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string sourcePath = "input.vsdx";

                // Path for the exported PDF file
                string outputPath = "output.pdf";

                // Load the diagram
                Diagram diagram = new Diagram(sourcePath);

                // Corporate standard margins (in inches)
                double topMargin = 0.5;
                double bottomMargin = 0.5;
                double leftMargin = 0.5;
                double rightMargin = 0.5;

                // Apply margins to every page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Access the PrintProps collection and set margin values
                    page.PageSheet.PrintProps.PageTopMargin.Value = topMargin;
                    page.PageSheet.PrintProps.PageBottomMargin.Value = bottomMargin;
                    page.PageSheet.PrintProps.PageLeftMargin.Value = leftMargin;
                    page.PageSheet.PrintProps.PageRightMargin.Value = rightMargin;
                }

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    // Ensure the format is explicitly set
                    SaveFormat = SaveFileFormat.Pdf,
                    // Optional: set a default font to handle missing fonts
                    DefaultFont = "Arial"
                };

                // Save the diagram as PDF with the configured options
                diagram.Save(outputPath, pdfOptions);

                // Clean up resources
                diagram.Dispose();

                Console.WriteLine("PDF export completed with custom margins.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }