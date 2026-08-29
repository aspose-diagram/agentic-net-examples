using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load the Visio diagram (replace with your actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Height threshold: 200 points = 200/72 inches
                double heightThresholdInches = 200.0 / 72.0;

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Check if the shape's height exceeds the threshold
                        if (shape.XForm.Height.Value > heightThresholdInches)
                        {
                            // Ensure the ThreeDFormat object exists before setting the rotation
                            if (shape.ThreeDFormat != null)
                            {
                                // Apply a 5‑degree rotation around the X‑axis
                                shape.ThreeDFormat.RotationXAngle.Value = 5.0;
                            }
                        }
                    }
                }

                // Prepare PDF save options (set a default font to avoid missing‑font issues)
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.DefaultFont = "Arial";

                // Export the modified diagram to PDF
                string outputPath = "output.pdf";
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine($"Diagram processed and saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }