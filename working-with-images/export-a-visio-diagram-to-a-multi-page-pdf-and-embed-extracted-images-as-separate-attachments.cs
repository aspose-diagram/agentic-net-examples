using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputPdfPath>");
            return;
        }

        // Input Visio file path.
        string visioPath = args[0];
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        // Output PDF file path.
        string pdfPath = args[1];
        // Ensure the directory for the PDF exists.
        string pdfDir = Path.GetDirectoryName(pdfPath);
        if (!string.IsNullOrEmpty(pdfDir) && !Directory.Exists(pdfDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {pdfDir}");
            return;
        }

        // Load the Visio diagram.
        Diagram diagram;
        try
        {
            diagram = new Diagram(visioPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load Visio file: {ex.Message}");
            return;
        }

        // Configure PDF save options (multi‑page PDF).
        PdfSaveOptions pdfOptions = new PdfSaveOptions
        {
            ExportHiddenPage = false,          // Do not export hidden pages.
            SaveFormat = SaveFileFormat.Pdf   // Explicitly set the format.
        };

        // Save the diagram as a PDF.
        try
        {
            diagram.Save(pdfPath, pdfOptions);
            Console.WriteLine($"Diagram saved as PDF: {pdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save PDF: {ex.Message}");
            return;
        }

        // --------------------------------------------------------------------
        // Extract image shapes (foreign objects) from the diagram.
        // --------------------------------------------------------------------
        var extractedImages = new List<(string FileName, byte[] Data)>();

        try
        {
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify image shapes by the Foreign type.
                    if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.Value != null)
                    {
                        // Build a unique file name for each image.
                        string imageFileName = $"Page{page.ID}_Shape{shape.ID}.png";

                        // Store the image data for later embedding.
                        extractedImages.Add((imageFileName, shape.ForeignData.Value));
                    }
                }
            }

            Console.WriteLine($"Extracted {extractedImages.Count} image(s) from the diagram.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during image extraction: {ex.Message}");
            return;
        }

        // --------------------------------------------------------------------
        // Embed the extracted images as attachments into the PDF.
        // --------------------------------------------------------------------
        try
        {
            // Load the previously saved PDF using Aspose.Pdf (fully qualified).
            var pdfDocument = new Aspose.Pdf.Document(pdfPath);

            // Iterate over each extracted image and add it as an embedded file.
            foreach (var (fileName, data) in extractedImages)
            {
                using (var ms = new MemoryStream(data))
                {
                    // Create a file specification for the attachment.
                    var fileSpec = new Aspose.Pdf.FileSpecification(ms, fileName);

                    // Add the attachment to the PDF's EmbeddedFiles collection.
                    pdfDocument.EmbeddedFiles.Add(fileSpec);
                }
            }

            // Save the PDF with the new attachments (overwrite the original file).
            pdfDocument.Save(pdfPath);
            Console.WriteLine($"Embedded {extractedImages.Count} image(s) into PDF: {pdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to embed images into PDF: {ex.Message}");
            return;
        }
    }
}