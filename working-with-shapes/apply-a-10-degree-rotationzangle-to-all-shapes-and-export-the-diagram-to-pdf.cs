using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram from a file (replace with actual path if needed)
                Diagram diagram = new Diagram("input.vsdx");

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Apply a 10‑degree rotation around the Z‑axis
                        // RotationZAngle is in degrees and accessed via the ThreeDFormat property
                        shape.ThreeDFormat.RotationZAngle.Value = 10;
                    }
                }

                // Configure PDF save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions
                {
                    // Ensure the format is explicitly set (required when using Aspose.Diagram save options)
                    SaveFormat = SaveFileFormat.Pdf,
                    // Optional: set a default font to avoid missing‑font issues
                    DefaultFont = "Arial"
                };

                // Save the modified diagram as a PDF file
                diagram.Save("output.pdf", pdfOptions);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }