using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;
using Aspose.Pdf.Facades;

class VisioToPdfConverter
{
    static void Main(string[] args)
    {
        try
        {

            // Paths of the Visio files to process
            string[] visioFiles = new string[] { "File1.vsdx", "File2.vsdx" };

            // Path for the final combined PDF
            string outputPdfPath = "Combined.pdf";

            // Temporary list to hold individual shape‑PDF file paths
            List<string> tempPdfFiles = new List<string>();

            foreach (string visioPath in visioFiles)
            {
                // Load the Visio diagram (uses the provided Diagram(string) constructor)
                using (Diagram diagram = new Diagram(visioPath))
                {
                    // Iterate through every page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through every shape on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Export the shape to a PDF stored in a memory stream
                            using (MemoryStream shapePdfStream = new MemoryStream())
                            {
                                shape.ToPdf(shapePdfStream);               // Uses Shape.ToPdf(Stream)
                                shapePdfStream.Position = 0;

                                // Write the stream to a temporary file (required for concatenation)
                                string tempFile = Path.Combine(Path.GetTempPath(),
                                    Guid.NewGuid().ToString() + ".pdf");
                                File.WriteAllBytes(tempFile, shapePdfStream.ToArray());
                                tempPdfFiles.Add(tempFile);
                            }
                        }
                    }
                }
            }

            // Concatenate all temporary shape PDFs into a single multi‑page PDF
            PdfFileEditor pdfEditor = new PdfFileEditor();
            pdfEditor.Concatenate(tempPdfFiles.ToArray(), outputPdfPath);

            // Clean up temporary files
            foreach (string tempFile in tempPdfFiles)
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }

            Console.WriteLine($"Combined PDF created at: {Path.GetFullPath(outputPdfPath)}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
