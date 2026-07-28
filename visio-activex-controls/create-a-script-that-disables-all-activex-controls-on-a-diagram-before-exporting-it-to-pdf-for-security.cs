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

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape contains an ActiveX control
                        if (shape.ActiveXControl != null)
                        {
                            // Mark the shape as deleted to disable the control
                            shape.Del = BOOL.True;
                        }
                    }
                }

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Set a default font to avoid missing font issues
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