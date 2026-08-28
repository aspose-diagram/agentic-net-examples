using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string visioPath = "input.vsdx";
                // Output PDF file path
                string pdfPath = "output.pdf";

                // Load the diagram
                Diagram diagram = new Diagram(visioPath);

                // Validate page orientation by checking width-to-height ratios
                foreach (Page page in diagram.Pages)
                {
                    double width = page.PageSheet.PageProps.PageWidth.Value;   // inches
                    double height = page.PageSheet.PageProps.PageHeight.Value; // inches

                    if (height == 0)
                    {
                        throw new Exception($"Page '{page.Name}' has zero height, cannot compute ratio.");
                    }

                    double ratio = width / height;

                    // Determine orientation based on ratio
                    string orientation = ratio > 1 ? "Landscape" : "Portrait";

                    Console.WriteLine($"Page '{page.Name}': Width={width}in, Height={height}in, Ratio={ratio:F2} => {orientation}");
                }

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial"; // fallback font for missing characters
                pdfOptions.ExportHiddenPage = false; // do not export hidden pages

                // Save the diagram as PDF
                diagram.Save(pdfPath, pdfOptions);

                Console.WriteLine($"Diagram successfully saved to PDF at '{pdfPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }