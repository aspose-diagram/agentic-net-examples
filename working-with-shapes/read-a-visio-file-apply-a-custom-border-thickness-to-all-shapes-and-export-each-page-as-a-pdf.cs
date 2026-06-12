using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string inputPath = "input.vsdx";

                // Verify the input file exists
                if (!File.Exists(inputPath))
                {
                    throw new FileNotFoundException($"Visio file not found: {inputPath}");
                }

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Desired border thickness (line weight) in inches
                    double borderThickness = 0.02; // approx 0.5 mm

                    // Apply the border thickness to every shape on every page
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Set line weight (border thickness)
                            shape.Line.LineWeight.Value = borderThickness;
                        }
                    }

                    // Export each page as a separate PDF file
                    int pageIndex = 0;
                    foreach (Page page in diagram.Pages)
                    {
                        // Prepare PDF save options for the current page
                        PdfSaveOptions pdfOptions = new PdfSaveOptions();
                        pdfOptions.DefaultFont = "Arial";
                        pdfOptions.ExportHiddenPage = false;
                        pdfOptions.PageIndex = pageIndex;   // zero‑based page index
                        pdfOptions.PageCount = 1;           // export only this page
                        pdfOptions.SaveFormat = SaveFileFormat.Pdf; // explicit format

                        // Build output file name
                        string outputFileName = $"{Path.GetFileNameWithoutExtension(inputPath)}_Page{pageIndex + 1}.pdf";

                        // Save the diagram (only the specified page) as PDF
                        diagram.Save(outputFileName, pdfOptions);

                        pageIndex++;
                    }
                }

                Console.WriteLine("Processing completed successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }