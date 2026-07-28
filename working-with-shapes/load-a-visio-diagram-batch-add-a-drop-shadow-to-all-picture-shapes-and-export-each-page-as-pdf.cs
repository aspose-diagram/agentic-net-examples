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
                string inputPath = "input.vsdx";

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    int pageIndex = 0;

                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Apply drop shadow to all picture (foreign) shapes on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Picture shapes are identified by TypeValue.Foreign
                            if (shape.Type == TypeValue.Foreign)
                            {
                                // Enable simple shadow
                                shape.Fill.ShapeShdwType.Value = ShapeShdwTypeValue.Simple;

                                // Shadow color (black)
                                shape.Fill.ShdwForegnd.Value = "#000000";

                                // Shadow transparency (30% transparent)
                                shape.Fill.ShdwForegndTrans.Value = 0.3;

                                // Shadow offset (in inches)
                                shape.Fill.ShapeShdwOffsetX.Value = 0.1;
                                shape.Fill.ShapeShdwOffsetY.Value = 0.1;
                            }
                        }

                        // Export the current page as a separate PDF file
                        string outputPdf = $"Page_{pageIndex + 1}.pdf";

                        PdfSaveOptions pdfOptions = new PdfSaveOptions
                        {
                            DefaultFont = "Arial",
                            PageIndex = pageIndex,   // zero‑based index of the page to render
                            PageCount = 1,           // render only this page
                            ExportHiddenPage = false
                        };

                        diagram.Save(outputPdf, pdfOptions);

                        pageIndex++;
                    }
                }

                Console.WriteLine("Processing completed. PDFs generated for each page.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }