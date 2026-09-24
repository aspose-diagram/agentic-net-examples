using System.IO;
using System;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.pdf";

            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Desired font for the watermark
            string watermarkFont = "Calibri";

            // Verify that the desired font is installed on the system
            bool fontInstalled = false;
            InstalledFontCollection fontCollection = new InstalledFontCollection();
            foreach (var family in fontCollection.Families)
            {
                // family.Name holds the font name
                if (string.Equals(family.Name, watermarkFont, StringComparison.OrdinalIgnoreCase))
                {
                    fontInstalled = true;
                    break;
                }
            }

            // If the font is missing, set a fallback default font for rendering
            if (!fontInstalled)
            {
                FontConfigs.DefaultFontName = "Arial";
                Console.WriteLine($"Font '{watermarkFont}' not found. Falling back to Arial.");
            }

            // Add watermark text to each page of the diagram
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Center coordinates for the watermark
                double pinX = pageWidth / 2;
                double pinY = pageHeight / 2;

                // Watermark properties
                string watermarkText = "CONFIDENTIAL";
                string fontColor = "#CCCCCC"; // Light gray
                double fontSizePoints = 36;   // Font size in points
                double fontSizeInches = fontSizePoints / 72.0; // Convert points to inches

                // Add a full‑page text shape as the watermark
                page.AddText(pinX, pinY, pageWidth, pageHeight,
                             watermarkText,
                             watermarkFont,
                             fontColor,
                             fontSizeInches);
            }

            // Configure PDF save options and ensure the default font is applied
            PdfSaveOptions pdfOptions = new PdfSaveOptions();
            pdfOptions.DefaultFont = FontConfigs.DefaultFontName;

            // Save the diagram with the watermark applied
            diagram.Save(outputPath, pdfOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
