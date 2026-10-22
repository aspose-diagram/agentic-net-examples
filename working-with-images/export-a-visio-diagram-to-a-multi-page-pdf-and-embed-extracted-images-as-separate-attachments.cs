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

        // Create a temporary directory for extracted images.
        string tempImageDir = Path.Combine(Path.GetTempPath(), "VisioImageExtract_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempImageDir);

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // -----------------------------------------------------------------
                // Extract foreign (image) shapes from each page and save them as PNG.
                // -----------------------------------------------------------------
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify image shapes by TypeValue.Foreign.
                        if (shape.Type == TypeValue.Foreign)
                        {
                            // Build a unique file name for the extracted image.
                            string imageFileName = $"Page{page.ID}_Shape{shape.ID}.png";
                            string imagePath = Path.Combine(tempImageDir, imageFileName);

                            // Configure image save options for PNG format.
                            ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);

                            // Export the shape to an image file.
                            shape.ToImage(imagePath, imgOptions);
                        }
                    }
                }

                // -----------------------------------------------------------------
                // Export the entire diagram to a multi‑page PDF.
                // -----------------------------------------------------------------
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.SaveFormat = SaveFileFormat.Pdf;   // Explicitly set format tracker.
                pdfOptions.ExportHiddenPage = false;          // Do not export hidden pages.

                // Save the PDF to the requested output path.
                diagram.Save(outputPdfPath, pdfOptions);
            }

            // -----------------------------------------------------------------
            // Embed the extracted images as attachments into the PDF.
            // -----------------------------------------------------------------
            // Load the generated PDF using Aspose.Pdf (fully qualified to avoid ambiguity).
            using (Aspose.Pdf.Document pdfDoc = new Aspose.Pdf.Document(outputPdfPath))
            {
                // Iterate over each extracted image file and add it as an embedded file.
                foreach (string imgFile in Directory.GetFiles(tempImageDir))
                {
                    using (FileStream imgStream = new FileStream(imgFile, FileMode.Open, FileAccess.Read))
                    {
                        // Create a file specification for the attachment.
                        Aspose.Pdf.FileSpecification fileSpec = new Aspose.Pdf.FileSpecification(imgStream, Path.GetFileName(imgFile));
                        // Add the attachment to the PDF's EmbeddedFiles collection.
                        pdfDoc.EmbeddedFiles.Add(fileSpec);
                    }
                }

                // Save the PDF with the new attachments (overwrite the original file).
                pdfDoc.Save(outputPdfPath);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // Clean up temporary images.
            try
            {
                if (Directory.Exists(tempImageDir))
                {
                    Directory.Delete(tempImageDir, true);
                }
            }
            catch
            {
                // Suppress any cleanup errors.
            }
        }
    }
}