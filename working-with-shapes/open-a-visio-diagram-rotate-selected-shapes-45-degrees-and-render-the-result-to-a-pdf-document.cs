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
                string outputPath = "rotated_output.pdf";

                // Load the Visio diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Rotate each non-deleted shape on every page by 45 degrees
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip shapes that are marked as deleted
                            if (shape.Del == BOOL.True)
                                continue;

                            // Get current rotation angle and add 45 degrees
                            double currentAngle = shape.XForm.Angle.Value;
                            shape.XForm.Angle.Value = currentAngle + 45.0;
                        }
                    }

                    // Configure PDF save options (optional: set default font)
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();
                    pdfOptions.DefaultFont = "Arial";

                    // Save the modified diagram as PDF
                    diagram.Save(outputPath, pdfOptions);
                }

                Console.WriteLine($"Diagram has been rotated and saved to PDF at: {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }