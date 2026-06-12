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

            // Input Visio file and size limit (in bytes)
            string inputPath = "input.vsdx";
            long maxAllowedSizeBytes = 5_000_000; // example: 5 MB

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Record original file size
            long originalSize = new FileInfo(inputPath).Length;

            // Add a watermark to each page
            foreach (Page page in diagram.Pages)
            {
                // Page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Center position for the watermark
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Use full page size for the text box so the watermark spans the page
                string watermarkText = "CONFIDENTIAL";
                string fontName = "Calibri";
                string fontColor = "#A5A5A5"; // light gray
                double fontSizeInches = 0.25; // approx 18 pt (18/72)

                // AddText returns a Shape representing the watermark
                page.AddText(pinX, pinY, pageWidth, pageHeight, watermarkText, fontName, fontColor, fontSizeInches);
            }

            // Save the modified diagram to a temporary file
            string outputPath = "output_with_watermark.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Get the new file size
            long newSize = new FileInfo(outputPath).Length;

            // Verify size does not exceed the specified limit
            if (newSize - originalSize > maxAllowedSizeBytes)
            {
                throw new Exception($"Watermark increased file size by {newSize - originalSize} bytes, exceeding the limit of {maxAllowedSizeBytes} bytes.");
            }
            else
            {
                Console.WriteLine($"Watermark added successfully. Size increase: {newSize - originalSize} bytes (within limit).");
            }

            // Clean up
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
