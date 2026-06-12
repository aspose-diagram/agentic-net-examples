using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Apply a simple drop shadow to all picture (foreign) shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Type == TypeValue.Foreign) // picture shape
                    {
                        // Enable simple shadow
                        shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;
                        // Shadow color (black)
                        shape.Fill.ShdwForegnd.Value = "#000000";
                        // Shadow transparency (30% transparent)
                        shape.Fill.ShdwForegndTrans.Value = 0.3;
                        // Shadow offset
                        shape.Fill.ShapeShdwOffsetX.Value = 0.1;
                        shape.Fill.ShapeShdwOffsetY.Value = 0.1;
                    }
                }

                // Export the current page as an individual PDF file
                string outputPath = $"Page_{page.ID}.pdf";

                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    // Export only this page
                    PageIndex = page.ID,   // zero‑based page index; using page.ID works for most diagrams
                    PageCount = 1,
                    // Explicitly set the format (required for ambiguous namespaces)
                    SaveFormat = SaveFileFormat.Pdf
                };

                diagram.Save(outputPath, pdfOptions);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
