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

                // Desired font size in points (10 pt) converted to inches (1 pt = 1/72 inch)
                double fontSizeInInches = 10.0 / 72.0;

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape contains any text
                        if (shape.Text != null && !string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                        {
                            // If the shape has no character entries, create one
                            if (shape.Chars.Count == 0)
                            {
                                Aspose.Diagram.Char newChar = new Aspose.Diagram.Char();
                                newChar.IX = 0;
                                newChar.Size.Value = fontSizeInInches;
                                shape.Chars.Add(newChar);
                            }
                            else
                            {
                                // Set the font size for each character run
                                foreach (Aspose.Diagram.Char ch in shape.Chars)
                                {
                                    ch.Size.Value = fontSizeInInches;
                                }
                            }
                        }
                    }
                }

                // Configure PDF save options (optional default font)
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";

                // Save the diagram as PDF
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine($"Diagram exported to PDF successfully: {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }