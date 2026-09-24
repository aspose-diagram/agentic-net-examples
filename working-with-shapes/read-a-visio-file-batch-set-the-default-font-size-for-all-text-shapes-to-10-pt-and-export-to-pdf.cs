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

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Desired font size in points (10 pt) converted to inches
                double fontSizeInInches = 10.0 / 72.0;

                // Iterate over all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Process only shapes that contain text
                        if (shape.Text != null && !string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                        {
                            // Set the font size for each character run
                            foreach (Aspose.Diagram.Char ch in shape.Chars)
                            {
                                ch.Size.Value = fontSizeInInches;
                            }
                        }
                    }
                }

                // Export the diagram to PDF
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial"; // fallback font
                diagram.Save(outputPath, pdfOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }