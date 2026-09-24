using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Input Visio file, output file and allowed size increase (in bytes)
            string inputPath = "input.vsdx";
            string outputPath = "output_with_watermark.vsdx";
            long maxAllowedIncrease = 1024; // e.g., 1 KB

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Get original file size
                long originalSize = new FileInfo(inputPath).Length;

                // Assume we add watermark to the first page
                Page page = diagram.Pages[0];

                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Add a full‑page watermark text
                // PinX and PinY are the lower‑left corner of the text box
                double pinX = 0;
                double pinY = 0;
                string watermarkText = "CONFIDENTIAL";
                string fontName = "Calibri";
                string fontColor = "#A5A5A5"; // light gray
                double fontSizeInPoints = 72; // 1 inch height
                double fontSizeInInches = fontSizeInPoints / 72.0;

                page.AddText(pinX, pinY, pageWidth, pageHeight, watermarkText, fontName, fontColor, fontSizeInInches);

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            // Get new file size
            long newSize = new FileInfo(outputPath).Length;
            long sizeIncrease = newSize - new FileInfo(inputPath).Length;

            // Verify the increase does not exceed the allowed limit
            if (sizeIncrease > maxAllowedIncrease)
            {
                throw new Exception($"Watermark added {sizeIncrease} bytes, which exceeds the allowed increase of {maxAllowedIncrease} bytes.");
            }
            else
            {
                Console.WriteLine($"Watermark added successfully. Size increase: {sizeIncrease} bytes (limit: {maxAllowedIncrease} bytes).");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
