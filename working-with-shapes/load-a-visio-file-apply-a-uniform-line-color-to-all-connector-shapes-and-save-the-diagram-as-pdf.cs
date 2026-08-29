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
                string inputPath = "input.vsdx";

                // Output PDF file path
                string outputPath = "output.pdf";

                // Load the Visio diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through all pages in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Identify connector shapes (1‑D shapes)
                            if (shape.OneD)
                            {
                                // Apply a uniform line color (red in this example)
                                shape.Line.LineColor.Value = "#FF0000";
                            }
                        }
                    }

                    // Configure PDF save options (optional: set a default font)
                    PdfSaveOptions pdfOptions = new PdfSaveOptions
                    {
                        DefaultFont = "Arial"
                    };

                    // Save the modified diagram as PDF
                    diagram.Save(outputPath, pdfOptions);
                }

                Console.WriteLine("Diagram saved as PDF with updated connector line colors.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }