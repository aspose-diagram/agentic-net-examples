using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.pdf";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the desired font color in HEX (RGB) format
                string titleFontColor = "#FF5733"; // Example RGB color

                // Iterate through all pages and shapes to find title shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify title shapes by their universal name containing "Title"
                        if (!string.IsNullOrEmpty(shape.NameU) && shape.NameU.Contains("Title"))
                        {
                            // Apply the font color to each character run in the shape
                            foreach (Aspose.Diagram.Char ch in shape.Chars)
                            {
                                ch.Color.Value = titleFontColor;
                            }
                        }
                    }
                }

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial"; // Fallback font
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