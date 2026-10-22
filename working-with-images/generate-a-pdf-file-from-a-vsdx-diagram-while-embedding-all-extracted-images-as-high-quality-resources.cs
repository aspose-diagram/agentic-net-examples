using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (can be passed as command‑line arguments)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.pdf";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the VSDX diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // -----------------------------------------------------------------
                // Extract all foreign (image) shapes from the diagram
                // -----------------------------------------------------------------
                List<byte[]> extractedImages = new List<byte[]>();

                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Foreign shapes contain image data
                        if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.Value != null)
                        {
                            extractedImages.Add(shape.ForeignData.Value);
                        }
                    }
                }

                // -----------------------------------------------------------------
                // Save the diagram to a temporary PDF file using PdfSaveOptions
                // -----------------------------------------------------------------
                string tempPdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".pdf");

                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                // Explicitly set the format tracker as required
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;

                // Save the diagram as PDF
                diagram.Save(tempPdfPath, pdfOptions);

                // -----------------------------------------------------------------
                // Open the generated PDF with Aspose.Pdf and embed extracted images
                // -----------------------------------------------------------------
                // Fully qualified Aspose.Pdf types are used to avoid namespace conflicts
                Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(tempPdfPath);

                int imageIndex = 1;
                foreach (byte[] imgBytes in extractedImages)
                {
                    using (MemoryStream imgStream = new MemoryStream(imgBytes))
                    {
                        // Create a file specification for the image and add it to the PDF's embedded files collection
                        string fileName = $"Image_{imageIndex}.png";
                        Aspose.Pdf.FileSpecification fileSpec = new Aspose.Pdf.FileSpecification(imgStream, fileName);
                        pdfDocument.EmbeddedFiles.Add(fileSpec);
                        imageIndex++;
                    }
                }

                // Save the final PDF with embedded images
                pdfDocument.Save(outputPath);

                // Clean up the temporary PDF file
                if (File.Exists(tempPdfPath))
                {
                    File.Delete(tempPdfPath);
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}