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
                // Desired title font color in HEX (RGB)
                string titleFontColor = "#FF5733";

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Identify title shapes by name (contains "Title")
                            if (!string.IsNullOrEmpty(shape.NameU) && shape.NameU.Contains("Title"))
                            {
                                // Apply the font color to each character in the shape
                                foreach (Aspose.Diagram.Char ch in shape.Chars)
                                {
                                    ch.Color.Value = titleFontColor;
                                }
                            }
                        }
                    }

                    // Configure PDF save options
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();
                    pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                    // Save the modified diagram as PDF
                    diagram.Save(outputPath, pdfOptions);
                }

                Console.WriteLine("Diagram titles font color updated and saved as PDF.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }