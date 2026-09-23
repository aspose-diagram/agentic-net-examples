using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths – adjust as needed
                string visioPath = "input.vsdx";
                string pdfPath = "output.pdf";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // Export the diagram to a multi‑page PDF
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";
                // Export hidden pages if required (set to false to exclude)
                pdfOptions.ExportHiddenPage = false;
                diagram.Save(pdfPath, pdfOptions);

                // Collect all embedded images from the diagram
                List<(string FileName, byte[] Data)> images = new List<(string, byte[])>();

                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify foreign (image) shapes
                        if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.Value != null)
                        {
                            // Use the shape ID to create a unique file name
                            string fileName = $"Image_Shape_{shape.ID}.png";
                            images.Add((fileName, shape.ForeignData.Value));
                        }
                    }
                }

                // Open the generated PDF with Aspose.Pdf (fully qualified to avoid namespace clash)
                Aspose.Pdf.Document pdfDocument = new Aspose.Pdf.Document(pdfPath);

                // Attach each extracted image to the PDF
                foreach (var img in images)
                {
                    using (MemoryStream ms = new MemoryStream(img.Data))
                    {
                        // Add the image as an embedded file (attachment)
                        pdfDocument.EmbeddedFiles.Add(new Aspose.Pdf.FileSpecification(ms, img.FileName));
                    }
                }

                // Save the final PDF with attachments (overwrites the previous file)
                pdfDocument.Save(pdfPath);

                Console.WriteLine("PDF export completed and images attached successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }