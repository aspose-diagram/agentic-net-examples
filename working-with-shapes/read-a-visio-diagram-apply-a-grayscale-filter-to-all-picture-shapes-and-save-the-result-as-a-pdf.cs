using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Input Visio file path
            string inputPath = "input.vsdx";
            // Output PDF file path
            string outputPath = "output.pdf";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify picture shapes (foreign type)
                    if (shape.Type == TypeValue.Foreign)
                    {
                        // Apply a grayscale effect by adjusting the gamma value
                        // (Gamma of 0.5 approximates a grayscale conversion)
                        shape.Image.Gamma.Value = 0.5;
                    }
                }
            }

            // Configure PDF save options (optional: set a default font)
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";

            // Save the modified diagram as PDF
            diagram.Save(outputPath, pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
