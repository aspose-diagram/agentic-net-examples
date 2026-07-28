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
                // Desired font color in hex (RGB)
                string titleFontColor = "#FF0000"; // Red

                try
                {
                    // Load the diagram
                    using (Diagram diagram = new Diagram(inputPath))
                    {
                        // Iterate through all pages
                        foreach (Page page in diagram.Pages)
                        {
                            // Iterate through all shapes on the page
                            foreach (Shape shape in page.Shapes)
                            {
                                // Identify title shapes (example: shape name contains "Title")
                                if (!string.IsNullOrEmpty(shape.NameU) &&
                                    shape.NameU.IndexOf("Title", StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    // Apply the font color to each character in the shape
                                    foreach (Aspose.Diagram.Char ch in shape.Chars)
                                    {
                                        ch.Color.Value = titleFontColor;
                                    }
                                }
                            }
                        }

                        // Configure PDF save options (optional: set a default font)
                        PdfSaveOptions pdfOptions = new PdfSaveOptions();
                        pdfOptions.DefaultFont = "Arial";

                        // Save the modified diagram as PDF
                        diagram.Save(outputPath, pdfOptions);
                    }

                    Console.WriteLine("Diagram processed and saved as PDF successfully.");
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