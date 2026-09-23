using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file path (change as needed)
            string inputPath = "input.vsdx";
            // Output PDF file path
            string outputPath = "output.pdf";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Define the desired title font color (hex RGB)
            string titleColorHex = "#FF0000"; // Red

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify title shapes by name containing "Title" (case‑insensitive)
                    if (shape.NameU != null && shape.NameU.IndexOf("Title", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        // Ensure the shape has text
                        if (shape.Text != null && !string.IsNullOrWhiteSpace(shape.Text.Value.ToString()))
                        {
                            // Apply the color to each character in the shape
                            foreach (Aspose.Diagram.Char ch in shape.Chars)
                            {
                                ch.Color.Value = titleColorHex;
                            }
                        }
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
