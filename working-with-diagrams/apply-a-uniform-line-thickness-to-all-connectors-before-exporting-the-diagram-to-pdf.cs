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
                // Path for the exported PDF
                string outputPath = "output.pdf";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Apply a uniform line thickness to all connector shapes (1‑D shapes)
                const double uniformLineWeight = 0.03; // thickness in inches

                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Connectors are 1‑D shapes; set their line weight
                        if (shape.OneD)
                        {
                            shape.Line.LineWeight.Value = uniformLineWeight;
                        }
                    }
                }

                // Configure PDF save options (optional settings can be added here)
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Example: set a default font to avoid missing‑font issues
                pdfOptions.DefaultFont = "Arial";

                // Save the diagram as PDF using the options
                diagram.Save(outputPath, pdfOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }