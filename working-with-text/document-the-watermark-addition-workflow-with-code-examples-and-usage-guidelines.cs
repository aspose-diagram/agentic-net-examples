using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // ------------------------------------------------------------
        // Usage guidelines:
        //   1. Provide the input Visio file path as the first argument.
        //   2. Provide the output Visio file path as the second argument (optional).
        //   3. The program adds a full‑page watermark text to each page.
        // ------------------------------------------------------------

        if (args.Length < 1)
        {
            Console.Error.WriteLine("Error: Input file path is required.");
            Console.Error.WriteLine("Usage: WatermarkDemo <inputVisioPath> [outputVisioPath]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length > 1 ? args[1] : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "Watermarked_" + Path.GetFileName(inputPath));

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and add a watermark
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Center position for the watermark
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Watermark text and appearance
                string watermarkText = "CONFIDENTIAL";
                string fontName = "Arial";
                string fontColor = "#C0C0C0"; // Light gray
                double fontSize = 0.5; // Size in inches

                // Add the watermark as a full‑page text shape
                // Parameters: pinX, pinY, width, height, text, fontName, fontColor, fontSize
                page.AddText(pinX, pinY, pageWidth, pageHeight, watermarkText, fontName, fontColor, fontSize);
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Watermark added successfully. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}