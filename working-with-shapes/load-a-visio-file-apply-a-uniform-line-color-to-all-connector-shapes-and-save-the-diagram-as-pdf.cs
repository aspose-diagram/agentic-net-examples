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

                // Define the desired line color (hex format)
                string lineColorHex = "#FF0000"; // Red

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Process only connector shapes (1‑D shapes)
                        if (shape.OneD)
                        {
                            // Apply the uniform line color
                            shape.Line.LineColor.Value = lineColorHex;
                        }
                    }
                }

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Save the modified diagram as PDF
                diagram.Save(outputPath, pdfOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }