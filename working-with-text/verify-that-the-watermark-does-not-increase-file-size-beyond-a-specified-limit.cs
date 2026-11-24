using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> [sizeLimitInBytes]");
            return;
        }

        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse optional size limit (default 1024 bytes)
        long sizeLimit = 1024;
        if (args.Length >= 2 && !long.TryParse(args[1], out sizeLimit))
        {
            Console.Error.WriteLine("Invalid size limit argument. It must be a numeric value.");
            return;
        }

        // Determine output (watermarked) file path
        string directory = Path.GetDirectoryName(inputPath) ?? "";
        string fileNameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
        string outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_watermarked.vsdx");

        try
        {
            // Load the original diagram
            Diagram diagram = new Diagram(inputPath);

            // Add watermark to each page
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Center position for the watermark
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Watermark text and styling
                string watermarkText = "Watermark";
                string fontName = "Calibri";
                string fontColor = "#a5a5a5";
                // Font size in points converted to inches (e.g., 36pt -> 0.5in)
                double fontSizeInPoints = 36;
                double fontSizeInInches = fontSizeInPoints / 72.0;

                // Add the watermark covering the full page
                page.AddText(pinX, pinY, pageWidth, pageHeight, watermarkText, fontName, fontColor, fontSizeInInches);
            }

            // Save the watermarked diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            return;
        }

        // Verify file size increase does not exceed the limit
        try
        {
            long originalSize = new FileInfo(inputPath).Length;
            long newSize = new FileInfo(outputPath).Length;
            long increase = newSize - originalSize;

            Console.WriteLine($"Original size: {originalSize} bytes");
            Console.WriteLine($"Watermarked size: {newSize} bytes");
            Console.WriteLine($"Size increase: {increase} bytes");

            if (increase <= sizeLimit)
            {
                Console.WriteLine("Watermark size increase is within the specified limit.");
            }
            else
            {
                Console.Error.WriteLine("Watermark size increase exceeds the specified limit.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error evaluating file sizes: {ex.Message}");
        }
    }
}