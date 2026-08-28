using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            // Create a new empty diagram (contains a default page)
            using (Diagram diagram = new Diagram())
            {
                // Iterate through all pages and set print orientation to Landscape
                foreach (Page page in diagram.Pages)
                {
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                    // Verify that the orientation was set correctly
                    if (page.PageSheet.PrintProps.PrintPageOrientation.Value != PrintPageOrientationValue.Landscape)
                    {
                        throw new Exception($"Failed to set Landscape orientation for page ID {page.ID}");
                    }

                    // Output page orientation to console for confirmation
                    Console.WriteLine($"Page ID {page.ID} orientation set to {page.PageSheet.PrintProps.PrintPageOrientation.Value}");
                }

                // Prepare PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    DefaultFont = "Arial"
                };

                // Define output PDF path
                string outputPdfPath = "output.pdf";

                // Save the diagram as PDF
                diagram.Save(outputPdfPath, pdfOptions);
                Console.WriteLine($"Diagram saved as PDF to '{Path.GetFullPath(outputPdfPath)}'.");

                // Verify that the PDF file was created
                if (!File.Exists(outputPdfPath))
                {
                    throw new Exception("PDF file was not created.");
                }

                // Simple verification that the PDF exists; further dimension checks would require
                // parsing the PDF, which is beyond the scope of this example.
                Console.WriteLine("PDF generation verified successfully.");
            }
        }
    }