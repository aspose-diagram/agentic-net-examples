using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
    {
        // Minimum font size in points for accessibility (e.g., 12pt)
        const double MinimumFontSize = 12.0;

        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.pdf";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Configure fallback font (used if a font is missing)
                FontConfigs.DefaultFontName = "Arial";

                // Validate font sizes across all shapes
                ValidateFontSizes(diagram);

                // Prepare PDF/A save options
                PdfSaveOptions pdfOptions = new PdfSaveOptions();
                pdfOptions.Compliance = PdfCompliance.PdfA1b; // PDF/A-1b compliance
                pdfOptions.DefaultFont = "Arial"; // Ensure a default font is set

                // Save the diagram as PDF/A
                diagram.Save(outputPath, pdfOptions);

                Console.WriteLine("Diagram exported to PDF/A successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        static void ValidateFontSizes(Diagram diagram)
        {
            // Iterate through each page
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Iterate through each character formatting run
                    foreach (Aspose.Diagram.Char ch in shape.Chars)
                    {
                        // Font size is stored in points
                        double fontSize = ch.Size.Value;

                        if (fontSize < MinimumFontSize)
                        {
                            // Log the issue
                            Console.WriteLine($"Shape ID {shape.ID} on page '{page.Name}' uses font size {fontSize}pt, which is below the minimum of {MinimumFontSize}pt.");

                            // Optionally, adjust the font size to meet the minimum
                            ch.Size.Value = MinimumFontSize;
                        }
                    }
                }
            }
        }
    }