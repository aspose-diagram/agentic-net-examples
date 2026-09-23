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

            // Input Visio file path
            string inputPath = "input.vsdx";
            // Output PDF file path
            string outputPath = "output.pdf";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Validate that no embedded image exceeds 2 MB
            const long maxSizeBytes = 2L * 1024 * 1024; // 2 megabytes

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify foreign (image) shapes
                    if (shape.Type == TypeValue.Foreign)
                    {
                        // Ensure the foreign data (image bytes) is present
                        if (shape.ForeignData != null && shape.ForeignData.Value != null)
                        {
                            long imageSize = shape.ForeignData.Value.Length;
                            if (imageSize > maxSizeBytes)
                            {
                                throw new Exception($"Image in shape ID {shape.ID} exceeds 2 MB (size: {imageSize} bytes).");
                            }
                        }
                    }
                }
            }

            // Export the diagram to PDF after validation
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = "Arial";
            diagram.Save(outputPath, pdfOptions);

            Console.WriteLine("Diagram exported successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
