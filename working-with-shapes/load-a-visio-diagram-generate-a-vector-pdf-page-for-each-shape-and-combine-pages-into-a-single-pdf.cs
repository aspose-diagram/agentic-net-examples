using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string visioPath = "input.vsdx";
                // Output combined PDF path
                string combinedPdfPath = "combined.pdf";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // Create a temporary folder to store individual shape PDFs
                string tempFolder = Path.Combine(Path.GetTempPath(), "ShapePdfs_" + Guid.NewGuid().ToString());
                Directory.CreateDirectory(tempFolder);

                try
                {
                    // Export each non-deleted shape to its own PDF file
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip shapes marked as deleted
                            if (shape.Del == BOOL.True)
                                continue;

                            string shapePdfPath = Path.Combine(tempFolder, $"shape_{shape.ID}.pdf");
                            shape.ToPdf(shapePdfPath);
                        }
                    }

                    // Combine all individual PDFs into a single PDF document
                    var combinedDoc = new Aspose.Pdf.Document();

                    // Get PDF files in alphabetical order for deterministic output
                    string[] pdfFiles = Directory.GetFiles(tempFolder, "*.pdf");
                    Array.Sort(pdfFiles);

                    foreach (string pdfFile in pdfFiles)
                    {
                        // Load the single-page PDF
                        var singleDoc = new Aspose.Pdf.Document(pdfFile);

                        // Append each page from the single-page PDF to the combined document
                        foreach (Aspose.Pdf.Page srcPage in singleDoc.Pages)
                        {
                            combinedDoc.Pages.Add(srcPage);
                        }
                    }

                    // Save the combined PDF
                    combinedDoc.Save(combinedPdfPath);
                    Console.WriteLine($"Combined PDF saved to: {combinedPdfPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                    throw;
                }
                finally
                {
                    // Clean up temporary files
                    if (Directory.Exists(tempFolder))
                    {
                        Directory.Delete(tempFolder, true);
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }