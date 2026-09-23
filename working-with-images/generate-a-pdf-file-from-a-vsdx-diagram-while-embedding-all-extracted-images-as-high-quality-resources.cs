using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file (VSDX) and output PDF paths
                string visioPath = "input.vsdx";
                string intermediatePdfPath = "temp_output.pdf";
                string finalPdfPath = "output_with_images.pdf";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath, LoadFileFormat.Vsdx);

                // Collect all foreign (image) shapes' binary data
                List<(byte[] Data, string FileName)> images = new List<(byte[] Data, string FileName)>();
                int imageCounter = 1;

                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.Value != null)
                        {
                            // Use a simple naming scheme for the extracted images
                            string fileName = $"Image_{imageCounter}.png";
                            images.Add((shape.ForeignData.Value, fileName));
                            imageCounter++;
                        }
                    }
                }

                // Export the diagram to PDF (images are already embedded as part of the diagram)
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";
                pdfOptions.SaveFormat = SaveFileFormat.Pdf; // explicit format tracking
                diagram.Save(intermediatePdfPath, pdfOptions);

                // Load the generated PDF using Aspose.Pdf (fully qualified to avoid namespace conflict)
                Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(intermediatePdfPath);

                // Embed each extracted image as an attached file (high‑quality resource)
                foreach (var img in images)
                {
                    using (MemoryStream ms = new MemoryStream(img.Data))
                    {
                        // The FileSpecification constructor takes a stream and a file name
                        Aspose.Pdf.FileSpecification fileSpec = new Aspose.Pdf.FileSpecification(ms, img.FileName);
                        pdfDocument.EmbeddedFiles.Add(fileSpec);
                    }
                }

                // Save the final PDF with embedded image resources
                pdfDocument.Save(finalPdfPath);

                Console.WriteLine($"PDF generated at: {finalPdfPath}");
                Console.WriteLine($"Embedded {images.Count} image(s) as resources.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }