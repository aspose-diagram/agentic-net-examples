using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (modify as needed)
                string inputPath = "input.vsdx";
                // Output PDF file path
                string outputPath = "locked_output.pdf";

                // Load the Visio diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Apply full protection to the shape
                            shape.Protection.LockMoveX.Value = BOOL.True;
                            shape.Protection.LockMoveY.Value = BOOL.True;
                            shape.Protection.LockWidth.Value = BOOL.True;
                            shape.Protection.LockHeight.Value = BOOL.True;
                            shape.Protection.LockRotate.Value = BOOL.True;
                            shape.Protection.LockVtxEdit.Value = BOOL.True;
                        }
                    }

                    // Configure PDF save options
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();
                    pdfOptions.SaveFormat = SaveFileFormat.Pdf;
                    // Optional: set a default font to avoid missing font issues
                    pdfOptions.DefaultFont = "Arial";

                    // Save the locked diagram as PDF
                    diagram.Save(outputPath, pdfOptions);
                }

                Console.WriteLine("Diagram locked and exported to PDF successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }