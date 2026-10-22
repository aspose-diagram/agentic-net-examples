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
            Console.Error.WriteLine("Usage: WatermarkUtility <inputVisioFile> [outputVisioFile]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length > 1 ? args[1] : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", Path.GetFileNameWithoutExtension(inputPath) + "_watermarked.vsdx");

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page and add a diagonal watermark
            foreach (Page page in diagram.Pages)
            {
                // Retrieve page dimensions
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Center position for the watermark shape
                double pinX = pageWidth / 2.0;
                double pinY = pageHeight / 2.0;

                // Add a text shape that spans the whole page
                // Overload: AddText(pinX, pinY, width, height, text, fontName, fontColor, fontSize)
                Shape watermark = page.AddText(pinX, pinY, pageWidth, pageHeight, "CONFIDENTIAL", "Arial", "#808080", 72);

                // Rotate the text 45 degrees to make it diagonal
                watermark.TextXForm.TxtAngle.Value = 45;

                // Set foreground transparency to 30% (30 out of 100)
                watermark.Fill.FillForegndTrans.Value = 30;

                // Optionally set text background transparency to 30%
                watermark.TextBlock.TextBkgndTrans.Value = 30;
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Watermarked diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}