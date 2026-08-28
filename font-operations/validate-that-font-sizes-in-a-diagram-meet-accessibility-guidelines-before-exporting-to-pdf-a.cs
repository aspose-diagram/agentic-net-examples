using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.pdf";

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Configure font folder(s) – required before rendering/saving
            // Adjust the path to a valid font directory on the target machine
            FontConfigs.SetFontFolder(@"C:\Windows\Fonts", true);
            // Set a fallback default font
            FontConfigs.DefaultFontName = "Arial";

            // Minimum accessible font size: 12 points (converted to inches)
            const double minSizeInInches = 12.0 / 72.0;
            bool hasInvalidFontSize = false;

            // Iterate through all pages, shapes, and character runs
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Examine each character's font size
                    foreach (Aspose.Diagram.Char ch in shape.Chars)
                    {
                        double sizeInInches = ch.Size.Value; // size is stored in inches
                        if (sizeInInches < minSizeInInches)
                        {
                            double sizeInPoints = sizeInInches * 72.0;
                            Console.WriteLine($"[Warning] Shape ID {shape.ID} contains font size {sizeInPoints:F1} pt, which is below the 12 pt minimum.");
                            hasInvalidFontSize = true;
                        }
                    }
                }
            }

            // Halt export if any font size violations are found
            if (hasInvalidFontSize)
                throw new Exception("One or more shapes contain font sizes smaller than the accessibility minimum of 12 points.");

            // Prepare PDF/A export options
            PdfSaveOptions pdfOptions = new PdfSaveOptions
            {
                // Ensure a default font is used for any missing glyphs
                DefaultFont = "Arial",
                // Do not export hidden pages
                ExportHiddenPage = false
                // AutoFitPageToDrawingContent property does not exist; omitted
                // ComplianceLevel can be set if the enum exists, e.g.:
                // ComplianceLevel = Aspose.Diagram.Saving.PdfComplianceLevel.PdfA1b
            };

            // Export the diagram to PDF/A
            diagram.Save(outputPath, pdfOptions);
            Console.WriteLine("Diagram exported successfully to PDF/A.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}