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

                // Configure default fallback font for rendering
                FontConfigs.DefaultFontName = "Arial";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape contains text
                        if (shape.Text != null && shape.Text.Value != null && shape.Text.Value.Count > 0)
                        {
                            // Iterate through character formatting entries
                            foreach (Aspose.Diagram.Char ch in shape.Chars)
                            {
                                // Set font size to 10 points (converted to inches)
                                ch.Size.Value = 10.0 / 72.0;
                            }
                        }
                    }
                }

                // Prepare PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
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