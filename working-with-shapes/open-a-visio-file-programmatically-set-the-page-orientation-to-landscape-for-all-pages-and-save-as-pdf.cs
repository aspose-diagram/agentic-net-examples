using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output PDF file path
                string outputPath = "output.pdf";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Set each page's orientation to Landscape
                foreach (Page page in diagram.Pages)
                {
                    // Access the PrintProps of the page and set orientation
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                }

                // Prepare PDF save options (optional: set default font)
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";

                // Save the diagram as PDF
                diagram.Save(outputPath, pdfOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }