using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.ActiveXControls;

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

                try
                {
                    // Load the diagram
                    Diagram diagram = new Diagram(inputPath);

                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // If the shape contains an ActiveX control, mark it as deleted
                            if (shape.ActiveXControl != null)
                            {
                                shape.Del = BOOL.True;
                            }
                        }
                    }

                    // Configure PDF save options
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();
                    pdfOptions.DefaultFont = "Arial";
                    pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                    // Save the diagram as PDF
                    diagram.Save(outputPath, pdfOptions);

                    Console.WriteLine("Diagram exported to PDF successfully with ActiveX controls disabled.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }