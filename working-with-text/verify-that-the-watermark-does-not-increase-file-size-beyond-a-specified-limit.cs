using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the original diagram and the output diagram with watermark
            string inputPath = "input.vsdx";
            string outputPath = "output_with_watermark.vsdx";

            // Maximum allowed increase in file size (bytes)
            const long maxIncreaseBytes = 10 * 1024; // 10 KB

            // Verify the input file exists
            if (!File.Exists(inputPath))
            {
                throw new FileNotFoundException($"Input file not found: {inputPath}");
            }

            // Record original file size
            long originalSize = new FileInfo(inputPath).Length;

            // Load the diagram, add a watermark, and save it
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Use the first page (index 0)
                Page page = diagram.Pages[0];

                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Center position for the watermark
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Add a full‑page watermark text
                // Parameters: pinX, pinY, width, height, text, fontName, fontColor (hex), fontSize (in inches)
                page.AddText(pinX, pinY, pageWidth, pageHeight, "CONFIDENTIAL", "Arial", "#CCCCCC", 0.5);

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            // Measure new file size
            long newSize = new FileInfo(outputPath).Length;
            long increase = newSize - originalSize;

            // Validate size increase against the limit
            if (increase > maxIncreaseBytes)
            {
                throw new Exception($"Watermark increased file size by {increase} bytes, exceeding the limit of {maxIncreaseBytes} bytes.");
            }
            else
            {
                Console.WriteLine($"Watermark added successfully. File size increased by {increase} bytes, which is within the allowed limit.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
