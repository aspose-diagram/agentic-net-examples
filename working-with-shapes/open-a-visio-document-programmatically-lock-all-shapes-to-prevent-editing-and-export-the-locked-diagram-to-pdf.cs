using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string inputPath = "input.vsdx";
                // Output PDF file path
                string outputPath = "locked_output.pdf";

                try
                {
                    // Load the Visio diagram
                    Diagram diagram = new Diagram(inputPath);

                    // Iterate through all pages and shapes to apply protection
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Apply lock protection to prevent editing
                            shape.Protection.LockMoveX.Value = BOOL.True;
                            shape.Protection.LockMoveY.Value = BOOL.True;
                            shape.Protection.LockWidth.Value = BOOL.True;
                            shape.Protection.LockHeight.Value = BOOL.True;
                            shape.Protection.LockRotate.Value = BOOL.True;
                            shape.Protection.LockVtxEdit.Value = BOOL.True;
                        }
                    }

                    // Configure PDF save options
                    PdfSaveOptions pdfOptions = new PdfSaveOptions
                    {
                        DefaultFont = "Arial",
                        SaveFormat = SaveFileFormat.Pdf
                    };

                    // Save the locked diagram as PDF
                    diagram.Save(outputPath, pdfOptions);

                    Console.WriteLine($"Diagram locked and saved to PDF successfully: {outputPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred:");
                    Console.WriteLine(ex.Message);
                    throw;
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }