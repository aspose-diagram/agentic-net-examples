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
        string outputPdfPath = args.Length > 1 ? args[1] : "output.pdf";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Collection to hold extracted image data and file names
        List<(byte[] Data, string FileName)> extractedImages = new List<(byte[], string)>();

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages and shapes to find foreign (image) shapes
                int imageIndex = 0;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify image shapes by TypeValue.Foreign and non‑null ForeignData
                        if (shape.Type == TypeValue.Foreign && shape.ForeignData?.Value != null)
                        {
                            imageIndex++;
                            byte[] imageBytes = shape.ForeignData.Value;
                            // Default to PNG extension; real format detection is omitted for brevity
                            string imageFileName = $"ExtractedImage_{imageIndex}.png";
                            extractedImages.Add((imageBytes, imageFileName));
                        }
                    }
                }

                // Configure PDF save options for high‑quality output
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;               // Explicitly set format tracker
                pdfOptions.HorizontalResolution = 300;                    // High DPI for raster content
                pdfOptions.VerticalResolution = 300;                      // High DPI for raster content
                pdfOptions.ExportHiddenPage = false;                      // Do not export hidden pages

                // Save the diagram as PDF
                diagram.Save(outputPdfPath, pdfOptions);
            }

            // After the PDF is created, embed the extracted images as file attachments
            using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(outputPdfPath))
            {
                foreach (var (Data, FileName) in extractedImages)
                {
                    using (MemoryStream ms = new MemoryStream(Data))
                    {
                        // Create a file specification for the image and add it to the PDF's embedded files collection
                        Aspose.Pdf.FileSpecification fileSpec = new Aspose.Pdf.FileSpecification(ms, FileName);
                        pdfDoc.EmbeddedFiles.Add(fileSpec);
                    }
                }

                // Save the updated PDF (overwrites the previous file)
                pdfDoc.Save(outputPdfPath);
            }

            Console.WriteLine($"PDF generated successfully: {outputPdfPath}");
            Console.WriteLine($"Embedded {extractedImages.Count} image(s) into the PDF.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}