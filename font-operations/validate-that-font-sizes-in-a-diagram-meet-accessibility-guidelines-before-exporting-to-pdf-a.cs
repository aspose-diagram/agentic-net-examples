using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                const string inputPath = "input.vsdx";
                // Path for the exported PDF/A file
                const string outputPath = "output.pdf";

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Minimum accessible font size in points (e.g., 12pt)
                    const double minFontSizePoints = 12.0;
                    // Convert points to inches because Aspose.Diagram stores size in inches
                    double minFontSizeInches = minFontSizePoints / 72.0;

                    // Iterate through all pages, shapes, and character runs
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            foreach (Aspose.Diagram.Char ch in shape.Chars)
                            {
                                double sizeInInches = ch.Size.Value;
                                if (sizeInInches < minFontSizeInches)
                                {
                                    double sizeInPoints = sizeInInches * 72.0;
                                    throw new Exception(
                                        $"Font size {sizeInPoints:F1}pt is smaller than the minimum {minFontSizePoints}pt " +
                                        $"in shape ID {shape.ID} on page \"{page.Name}\".");
                                }
                            }
                        }
                    }

                    // Configure PDF save options (PDF/A compliance can be set via additional options if needed)
                    PdfSaveOptions pdfOptions = new PdfSaveOptions();
                    pdfOptions.SaveFormat = SaveFileFormat.Pdf;
                    // Set a fallback font to avoid missing‑font issues during export
                    pdfOptions.DefaultFont = "Arial";

                    // Export the diagram to PDF/A
                    diagram.Save(outputPath, pdfOptions);
                }

                Console.WriteLine("Diagram exported successfully after font size validation.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }