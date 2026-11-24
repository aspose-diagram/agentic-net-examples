using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputPdfPath>");
            return;
        }

        string inputPath = args[0];
        string outputPdfPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists.
        string outputDir = Path.GetDirectoryName(outputPdfPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Convert Visio diagram to a multi‑page PDF.
        // -----------------------------------------------------------------
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Configure PDF save options.
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.ExportHiddenPage = false;               // Do not include hidden pages.
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;        // Explicitly set the format.

                // Save the diagram as PDF.
                diagram.Save(outputPdfPath, pdfOptions);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during Visio‑to‑PDF conversion: {ex.Message}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 2: Load the generated PDF and embed extracted images as attachments.
        // -----------------------------------------------------------------
        try
        {
            // Load the PDF using Aspose.Pdf (fully qualified to avoid ambiguity).
            Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(outputPdfPath);

            // Re‑open the Visio diagram to extract foreign (image) shapes.
            using (Diagram diagram = new Diagram(inputPath))
            {
                int pageIndex = 0;
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    pageIndex++;

                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Identify foreign image shapes.
                        if (shape.Type == TypeValue.Foreign)
                        {
                            // Ensure the shape contains image data.
                            if (shape.ForeignData != null && shape.ForeignData.Value != null && shape.ForeignData.Value.Length > 0)
                            {
                                // Create a unique attachment name.
                                string attachmentName = $"Image_Page{pageIndex}_Shape{shape.ID}.bin";

                                // Create a memory stream from the image bytes.
                                using (MemoryStream imageStream = new MemoryStream(shape.ForeignData.Value))
                                {
                                    // Create a file specification for the attachment.
                                    Aspose.Pdf.FileSpecification fileSpec = new Aspose.Pdf.FileSpecification(imageStream, attachmentName);

                                    // Add the attachment to the PDF.
                                    pdfDocument.EmbeddedFiles.Add(fileSpec);
                                }
                            }
                        }
                    }
                }
            }

            // Save the PDF with the embedded images (overwrite the original file).
            pdfDocument.Save(outputPdfPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error embedding images into PDF: {ex.Message}");
            return;
        }

        Console.WriteLine($"PDF successfully created at: {outputPdfPath}");
    }
}